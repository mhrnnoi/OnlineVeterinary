using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

            // services.Configure<DbSettings>(configurationManager.GetSection(DbSettings.SectionName));
            services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IPetRepository, PetRepository>();
            // services.AddScoped<IDoctorRepository, DoctorRepository>();
            // services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            var jwtSettings = new JwtSettings();
            configurationManager.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            var dbSettings = new DbSettings();
            configurationManager.Bind(DbSettings.SectionName, dbSettings);
            services.AddSingleton(Options.Create(dbSettings));


            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
            services.AddScoped<IDateTimeProvider, DatetimeProvider>();
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseNpgsql("Server=127.0.0.1; Port =5432; User Id = postgres; password = Mehran123; database = OnlineVeterinary");
            });

            return services;
        }
    }
}
