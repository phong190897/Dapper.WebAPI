using Dapper.Application.IRepositories;
using Dapper.Application.IRepositories.IRepositories;
using Dapper.Core.CustomEntities;
using Dapper.Infrastructure.Filters.Swagger;
using Dapper.Infrastructure.Options;
using Dapper.Infrastructure.Repositories;
using Dapper.Infrastructure.Repositories.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dapper.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Dapper.WebAPI v1",
                    Version = "v1",
                    Description = "API V1 Description"
                });

                //options.SwaggerDoc("v2", new OpenApiInfo
                //{
                //    Title = "StarDesign API v2",
                //    Version = "v2",
                //    Description = "API V2 Description"

                //});

                options.ResolveConflictingActions(c => c.First());
                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                options.IncludeXmlComments(xmlPath);
                options.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOption>(options => configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOption>(options => configuration.GetSection("PasswordOptions").Bind(options));

            return services;
        }
    }
}
