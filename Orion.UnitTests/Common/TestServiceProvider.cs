using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orion.Application;
using Orion.Application.Common.Interfaces;
using Orion.Infrastructure;
using Orion.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.UnitTests
{
    public class TestServiceProvider : IDisposable
    {
        private ServiceCollection serviceCollection = null;
        private ServiceProvider serviceProvider = null;

        public string DatabaseName { get; private set; }

        public TestServiceProvider() : this($"db-{Guid.NewGuid().ToString().Substring(0, 8)}")
        {

        }

        public IMediator Mediator
        {
            get
            {
                return GetService<IMediator>();
            }
        }

        public TestServiceProvider(string dbName, Action<ServiceCollection> additionalServices = null)
        {
            var _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            DatabaseName = dbName;

            serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IConfiguration>(p => _configuration);

            serviceCollection.AddApplication();
            serviceCollection.AddLogging();
            serviceCollection.AddInfrastructure(_configuration);

            //serviceCollection.AddDbContext<OrionDbContext>(opt =>
            //{
            //    opt.UseInMemoryDatabase(databaseName: DatabaseName);
            //    opt.ConfigureWarnings(a => a.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            //}, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            //serviceCollection.AddScoped<IOrionDbContext>(provider => provider.GetService<OrionDbContext>());
            //serviceCollection.AddScoped<DbContext>(provider => provider.GetService<OrionDbContext>());

            if (additionalServices != null)
            {
                additionalServices.Invoke(serviceCollection);
            }

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                var _dbContext = GetService<DbContext>();

                serviceProvider.Dispose();

                _dbContext.Dispose();
            }
        }
    }
}
