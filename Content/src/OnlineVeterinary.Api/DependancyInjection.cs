using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Serilog;

namespace OnlineVeterinary.Api
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();
            return services;
        }
    }
}