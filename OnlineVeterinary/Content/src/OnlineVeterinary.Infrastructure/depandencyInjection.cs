using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Infrastructure.Mapping;
using OnlineVeterinary.Infrastructure.Persistence;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;
using OnlineVeterinary.Infrastructure.Services;

namespace OnlineVeterinary.Infrastructure
{
    public static class depandencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurationManager
        )
        {
            services.AddMapping();
            services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));
            services.Configure<DbSettings>(configurationManager.GetSection(DbSettings.SectionName));
            services.AddScoped<ICareGiverRepository, CareGiverRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IDateTimeProvider, DatetimeProvider>();
            services.AddDbContext<AppDbContext>();

            return services;
        }
    }
}
