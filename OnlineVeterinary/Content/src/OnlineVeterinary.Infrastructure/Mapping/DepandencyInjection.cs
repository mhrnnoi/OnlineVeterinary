using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineVeterinary.Application.Common;
using OnlineVeterinary.Application.Features.Auth.Commands.Register;
using OnlineVeterinary.Application.Features.Auth.Common;
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
            // config.NewConfig<UpdatePetCommand, Pet>().Map(dest => dest.PetType, src => (PetType)Enum.Parse(typeof(PetType), src.PetType.ToString()));
            // config.NewConfig<Pet, PetDTO>().Map(dest => dest.PetType, src => (PetType)src.PetType);

            config.NewConfig<RegisterCommand, User>().Map(dest => dest.Role, src => RoleValue(src.Role));
            config.NewConfig<(User, Jwt), AuthResult>().Map(dest => dest, src => src.Item1)
                            .Map(dest => dest.Token, src => src.Item2.token);



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
            return ((RoleEnum)enumValue).ToString();
        }
    }
}