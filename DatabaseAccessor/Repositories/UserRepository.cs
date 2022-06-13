﻿using DatabaseAccessor.Contexts;
using DatabaseAccessor.Mapping;
using DatabaseAccessor.Models;
using DatabaseAccessor.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAccessor.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly Mapper _mapper;

        public UserRepository(ApplicationDbContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserDTO>> FindUsersAsync(string keyword,
            PaginationInfo paginationInfo, string roleName = null)
        {
            var roles = typeof(SystemConstant.Roles).GetFields()
                .Select(property => (string)property.GetValue(null))
                .ToList();
            if (!roles.Contains(roleName) && roleName != null)
            {
                throw new ArgumentException($"{roleName} is not available", nameof(roleName));
            }
            IQueryable<User> query = _dbContext.Users.AsNoTracking()
                .Include(e => e.UserRoles)
                .ThenInclude(e => e.Role)
                .Include(e => e.AffectedReports);
            if (roleName != null)
                query = query.Where(e => e.UserRoles.Any(userRole => userRole.Role.Name == roleName));
            else
                query = query.Where(e =>
                    e.UserRoles.Any(userRole => userRole.Role.Name != SystemConstant.Roles.ADMIN_TEAM_13 &&
                        userRole.Role.Name != SystemConstant.Roles.ADMIN_TEAM_5));
            if (keyword != null)
                query = query.Where(e => e.Email.Contains(keyword));
            return await query
                .OrderBy(e => e.Status)
                .Select(e => _mapper.MapToUserDTO(e))
                .AsSplitQuery()
                .PaginateAsync(paginationInfo.PageNumber, paginationInfo.PageSize);
        }

        public async Task<CommandResponse<bool>> ApplyBanAsync(Guid userId, uint? dayCount, string banReason)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
                return CommandResponse<bool>.Error("User not found", null);

            if (user.LockoutEnd >= DateTimeOffset.Now)
                return CommandResponse<bool>.Error("User is already ban", null);

            user.LockoutEnd = dayCount.HasValue ? DateTimeOffset.Now.AddDays((double)dayCount) : null;

            user.Status = AccountStatus.Banned;
            user.BanReason = banReason;

            await _dbContext.SaveChangesAsync();

            return CommandResponse<bool>.Success(true);
        }

        public async Task<CommandResponse<bool>> UnbanAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
                return CommandResponse<bool>.Error("User is not found", null);

            if (user.LockoutEnd < DateTimeOffset.Now || user.Status == AccountStatus.Available)
                return CommandResponse<bool>.Error("User does not need to unban", null);

            user.LockoutEnd = null;
            user.Status = AccountStatus.Available;
            user.BanReason = null;

            await _dbContext.SaveChangesAsync();

            return CommandResponse<bool>.Success(true);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            return _mapper.MapToUserDTO(user);
        }

        public async Task<CommandResponse<bool>> AssignShopOwnerAsync(Guid userId, int shopId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            var shopStatus = await _dbContext.ShopStatus.FindAsync(shopId);
            if (shopStatus != null)
                return CommandResponse<bool>.Error("Invalid request", null);
            if (user.Status == AccountStatus.Banned)
                return CommandResponse<bool>.Error("User is banned", null);
            if (user.ShopId != null)
                return CommandResponse<bool>.Error("User is already belong to another shop", null);
            var response = await MoveToRoleAsync(user, SystemConstant.Roles.SHOP_OWNER);
            user.ShopId = shopId;
            _dbContext.ShopStatus.Add(new ShopStatus { ShopId = shopId });
            await _dbContext.SaveChangesAsync();
            return response;
        }

        public async Task<CommandResponse<bool>> AuthorizeUserAsync(Guid userId, bool authorizeToAdmin, bool team5)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user.ShopId != null)
                return CommandResponse<bool>.Error("ShopOwner can't be an system admin", null);
            return await MoveToRoleAsync(user,
                    authorizeToAdmin
                    ? (team5 ? SystemConstant.Roles.ADMIN_TEAM_5 : SystemConstant.Roles.ADMIN_TEAM_13)
                    : SystemConstant.Roles.CUSTOMER, true);
        }

        private async Task<CommandResponse<bool>> MoveToRoleAsync(User user, string roleName, bool shouldSave = false)
        {
            if (user == null)
                return CommandResponse<bool>.Error("User not found", null);
            if (roleName != SystemConstant.Roles.CUSTOMER && roleName != SystemConstant.Roles.SHOP_OWNER
                && roleName != SystemConstant.Roles.ADMIN_TEAM_13 && roleName != SystemConstant.Roles.ADMIN_TEAM_5)
                throw new ArgumentException("Specified role is not supported", new NotSupportedException());
            if (user.UserRoles[0].Role.Name == roleName)
                return CommandResponse<bool>.Error("User is already in role", null);
            _dbContext.UserRoles.Remove(user.UserRoles[0]);
            await _dbContext.SaveChangesAsync();
            user.UserRoles.Add(new UserRole
            {
                Role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.Name == roleName)
            });
            if (shouldSave)
                await _dbContext.SaveChangesAsync();
            return CommandResponse<bool>.Success(true);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
