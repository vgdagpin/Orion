using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orion.Application.Common.Interfaces;
using Orion.Infrastructure.Persistence;
using System;

namespace Orion.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrionDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: configuration.GetConnectionString("OrionDBConStr"),
                    sqlServerOptionsAction: opt => opt.MigrationsAssembly("Orion.DbMigration"));
            });

            services.AddScoped<IOrionDbContext>(provider => provider.GetService<OrionDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<OrionDbContext>());

            return services;
        }
    }
}