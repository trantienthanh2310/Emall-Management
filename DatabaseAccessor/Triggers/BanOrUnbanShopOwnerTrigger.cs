﻿using DatabaseAccessor.Contexts;
using DatabaseAccessor.Models;
using EFCore.BulkExtensions;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseAccessor.Triggers
{
    public class BanOrUnbanShopOwnerTrigger : IAfterSaveTrigger<User>, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public BanOrUnbanShopOwnerTrigger(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AfterSave(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Modified && context.Entity.ShopId != null)
            {
                await _dbContext.ShopProducts
                    .IgnoreQueryFilters()
                    .Where(product => product.ShopId == context.Entity.ShopId)
                    .BatchUpdateAsync(new ShopProduct
                    {
                        IsVisible = context.Entity.Status != AccountStatus.Banned
                    }, new List<string> { "IsVisible" }, cancellationToken);

                await _dbContext.ShopStatus
                    .Where(shop => shop.ShopId == context.Entity.ShopId)
                    .BatchUpdateAsync(new ShopStatus
                    {
                        IsDisabled = context.Entity.Status == AccountStatus.Banned
                    }, new List<string> { "IsDisabled" }, cancellationToken);
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
