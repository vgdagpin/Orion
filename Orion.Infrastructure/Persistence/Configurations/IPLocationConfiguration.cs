using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Infrastructure.Persistence.Configurations
{

    public class IPLocationConfiguration : IEntityTypeConfiguration<IPLocation>
    {
        public void Configure(EntityTypeBuilder<IPLocation> builder)
        {
            builder.HasIndex(a => a.IPFrom);
            builder.HasIndex(a => a.IPTo);

            builder.Property(a => a.CountryCode).HasMaxLength(2).IsRequired();
            builder.Property(a => a.CountryName).HasMaxLength(64).IsRequired();
            builder.Property(a => a.RegionName).HasMaxLength(128).IsRequired();
            builder.Property(a => a.CityName).HasMaxLength(128).IsRequired();
        }
    }
}
