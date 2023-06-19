using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Application.Pets.Commands.Add;
using OnlineVeterinary.Application.Pets.Commands.Update;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Pet.Enums;

namespace OnlineVeterinary.Infrastructure.Mapping
{
    public static class DepandencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.NewConfig<AddPetCommand,Pet>().Map(dest => dest.PetType, src => (PetType) Enum.Parse(typeof(PetType), src.PetType.ToString()));
            config.NewConfig<UpdatePetCommand,Pet>().Map(dest => dest.PetType, src => (PetType) Enum.Parse(typeof(PetType), src.PetType.ToString()));
            config.NewConfig<Pet,PetDTO>().Map(dest => dest.PetType, src => (PetType)src.PetType);
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}