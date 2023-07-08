using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            var jwtSettings = new JwtSettings();
            configurationManager.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddMapping();
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IDateTimeProvider, DatetimeProvider>();
            services.AddSingleton(Options.Create(jwtSettings));


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


            return services;
        }
    }
}
