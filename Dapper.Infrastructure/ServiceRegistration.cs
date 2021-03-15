﻿using Dapper.Application.IRepositories;
using Dapper.Application.IRepositories.IRepositories;
using Dapper.Infrastructure.Repositories;
using Dapper.Infrastructure.Repositories.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dapper.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
