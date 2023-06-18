using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Infrastructure.Mapping;
using OnlineVeterinary.Infrastructure.Persistence;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure
{
    public static class depandencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services
        )
        {
            services.AddMapping();
            services.AddScoped<ICareGiverRepository, CareGiverRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>();

            return services;
        }
    }
}
