﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Config
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P=> P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
            builder.Property(P => P.PictureUrl).IsRequired();
            builder.HasOne(P => P.Brand).WithMany().HasForeignKey(P => P.BrandId)
                //.OnDelete(DeleteBehavior.SetNull)
                ;
            builder.HasOne(P => P.Category).WithMany().HasForeignKey(P => P.CategoryId);

        }
    }
}
