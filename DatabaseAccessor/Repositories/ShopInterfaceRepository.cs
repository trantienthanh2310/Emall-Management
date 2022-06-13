﻿using DatabaseAccessor.Contexts;
using DatabaseAccessor.Mapping;
using DatabaseAccessor.Models;
using DatabaseAccessor.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Models;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAccessor.Repositories
{
    public class ShopInterfaceRepository : IShopInterfaceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mapper _mapper;

        public ShopInterfaceRepository(ApplicationDbContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ShopInterfaceDTO>> FindShopInterfaceByShopIdAsync(int shopId)
        {
            var shopInterface = await _dbContext.ShopInterfaces
                .AsNoTracking().FirstOrDefaultAsync(e => e.ShopId == shopId);
            return CommandResponse<ShopInterfaceDTO>.Success(_mapper.MapToShopInterfaceDTO(shopInterface));
        }

        public async Task<CommandResponse<ShopInterfaceDTO>> EditShopInterfaceAsync(int shopId,
            CreateOrEditInterfaceRequestModel requestModel)
        {
            var shopStatus = await _dbContext.ShopStatus.AsNoTracking()
                .FirstOrDefaultAsync(shop => shop.ShopId == shopId && !shop.IsDisabled);
            if (shopStatus == null)
                return CommandResponse<ShopInterfaceDTO>.Error("Shop is already disabled!", null);
            var shopInterface = await _dbContext.ShopInterfaces.FirstOrDefaultAsync(e => e.ShopId == shopId);
            if (shopInterface == null)
            {
                shopInterface = new ShopInterface().AssignByRequestModel(requestModel);
                shopInterface.ShopId = shopId;
                _dbContext.ShopInterfaces.Add(shopInterface);
            }
            else
            {
                shopInterface.AssignByRequestModel(requestModel);
                _dbContext.Entry(shopInterface).State = EntityState.Modified;
            }
            try
            {
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                return CommandResponse<ShopInterfaceDTO>.Error(ex.Message, ex);
            }
            return CommandResponse<ShopInterfaceDTO>.Success(_mapper.MapToShopInterfaceDTO(shopInterface));
        }

        public async Task<Dictionary<int, string>> GetShopAvatar(int[] shopIds)
        {
            return await _dbContext.ShopInterfaces
                .AsNoTracking()
                .Select(e => new { e.ShopId, e.Avatar })
                .Where(e => shopIds.Contains(e.ShopId))
                .ToDictionaryAsync(e => e.ShopId, e => e.Avatar);
        }

        public async Task<Dictionary<int, ShopInterfaceDTO>> GetShopInterfacesAsync(List<int> shopIds)
        {
            return await _dbContext.ShopInterfaces
                .AsNoTracking()
                .Where(e => shopIds.Contains(e.ShopId))
                .ToDictionaryAsync(e => e.ShopId, e => _mapper.MapToShopInterfaceDTO(e));
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}