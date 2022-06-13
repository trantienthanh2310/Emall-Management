﻿using DatabaseAccessor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;

namespace DatabaseAccessor.Configurations
{
    public class InvoiceConfiguration : BaseEntityConfiguration<Invoice>
    {
        private readonly bool _isForTestingPurpose;

        public InvoiceConfiguration(bool isForTestingPurpose = false)
        {
            _isForTestingPurpose = isForTestingPurpose;
        }

        public override bool IsForTestingPurpose => _isForTestingPurpose;

        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasIdentityOptions(0, 1);

            builder.HasOne(e => e.User)
                .WithMany(e => e.Invoices)
                .IsRequired();

            builder.HasIndex(e => e.UserId);
            builder.HasIndex(e => e.CreatedAt);
            builder.HasIndex(e => e.InvoiceCode);

            builder.HasMany(e => e.Details)
                .WithOne(e => e.Invoice)
                .IsRequired();

            if (!IsForTestingPurpose)
            {
                builder.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("getdate()");
            }

            builder.Property(e => e.Status)
                .HasDefaultValue(InvoiceStatus.New);

            builder.Property(e => e.IsPaid)
                .HasDefaultValue(false);
        }
    }
}
