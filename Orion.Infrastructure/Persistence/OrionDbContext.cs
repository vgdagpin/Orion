using Microsoft.EntityFrameworkCore;
using Orion.Application.Common.Interfaces;
using Orion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orion.Infrastructure.Persistence
{
    public class OrionDbContext : DbContext, IOrionDbContext
    {
        public DbSet<IPLocation> IPLocations { get; set; }

        public OrionDbContext(DbContextOptions<OrionDbContext> dbContextOpt) : base(dbContextOpt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrionDbContext).Assembly);

            /// Set all decimal SQL DataType
            foreach (var _property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                _property.SetColumnType("DECIMAL(38,0)");
            }
        }
    }
}
