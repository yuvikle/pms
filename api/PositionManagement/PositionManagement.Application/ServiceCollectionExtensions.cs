using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Application.Commands;
using PositionManagement.Application.Handlers;
using PositionManagement.Application.Validator;
using PositionManagement.Infrastructure.Context;
using PositionManagement.Infrastructure.Repositories;
using PositionManagement.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITradeRepository, TradeRepository>();

            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPositionsHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTradeHandler).Assembly));

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<ITradeValidator, TradeValidator>();

            return services;
        }
    }
}
