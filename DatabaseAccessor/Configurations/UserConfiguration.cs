﻿using DatabaseAccessor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;

namespace DatabaseAccessor.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        private readonly bool _isForTestingPurpose;

        public UserConfiguration(bool isForTestingPurpose = false)
        {
            _isForTestingPurpose = isForTestingPurpose;
        }

        public override bool IsForTestingPurpose => _isForTestingPurpose;

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Status).HasDefaultValue(AccountStatus.Available);
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
