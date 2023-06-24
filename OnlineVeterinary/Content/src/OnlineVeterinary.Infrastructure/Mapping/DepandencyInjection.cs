using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineVeterinary.Application.Features.Auth.Commands.Register;
using OnlineVeterinary.Application.Features.Pets.Commands.Add;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Pet.Enums;
using OnlineVeterinary.Domain.Role.Enums;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Infrastructure.Mapping
{
    public static class DepandencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<AddPetCommand, Pet>().Map(dest => dest.PetType, src => PetType(src.PetType));
            config.NewConfig<RegisterCommand, User>().Map(dest => dest.Role, src => RoleValue(src.Role));
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }

        private static string PetType(int enumValue)
        {
            return ((PetType)enumValue).ToString();
        }


        private static string RoleValue(int enumValue)
        {
            return ((RoleEnum)(enumValue)).ToString();
        }
    }
}