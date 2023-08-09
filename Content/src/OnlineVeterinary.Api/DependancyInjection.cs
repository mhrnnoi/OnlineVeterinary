using System.Globalization;
using Mapster;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using OnlineVeterinary.Api.Filters;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineVeterinary.Api
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped<LoginActionFilterAttribute>();
            services.AddScoped<ReservationFilterAttribute>();
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalFilter>();
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(AddAuthorizeToSwagger());

            services.AddSwaggerGen();
            return services;
        }

        private static Action<SwaggerGenOptions> AddAuthorizeToSwagger()
    {
        return option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        };

    }
    }
}