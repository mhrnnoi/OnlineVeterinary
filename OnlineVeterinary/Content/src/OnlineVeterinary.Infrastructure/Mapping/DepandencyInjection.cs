using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.Auth.Register;
using OnlineVeterinary.Application.Common;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Application.Pets.Commands.Add;
using OnlineVeterinary.Application.Pets.Commands.Update;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Pet.Enums;
using OnlineVeterinary.Infrastructure.Services;

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
            config.NewConfig<(User, Jwt),AuthResult>().Map(dest => dest , src => src.Item1)
                            .Map(dest => dest.Token , src => src.Item2.token);
            


            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}