using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineVeterinary.Models;
using OnlineVeterinary.Models.DTOs;
using OnlineVeterinary.Models.DTOs.Incoming;

namespace OnlineVeterinary.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Pet, PetDTO>()
            .ForMember(dest => 
            dest.FullName , opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => 
            dest.Username , opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => 
            dest.DateOfBirth , opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => 
            dest.PetType , opt => opt.MapFrom(src => src.PetType))
            .ForMember(dest => 
            dest.Sickness , opt => opt.MapFrom(src => src.Sickness));

            CreateMap<PetRegisterDTO, Pet>()
            .ForMember(dest => 
            dest.FullName , opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => 
            dest.Username , opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => 
            dest.DateOfBirth , opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => 
            dest.PetType , opt => opt.MapFrom(src => src.PetType))
            .ForMember(dest => 
            dest.Sickness , opt => opt.MapFrom(src => src.Sickness));
           
        }
        
    }
}