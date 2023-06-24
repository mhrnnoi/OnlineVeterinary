using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineVeterinary.Data.Entity;
using OnlineVeterinary.Models;
using OnlineVeterinary.Models.DTOs;

namespace OnlineVeterinary.Profiles
{
    public class CareGiverProfile : Profile
    {
        public CareGiverProfile()
        {


            CreateMap<CareGiver, CareGiverDTO>()
                        .ForMember(dest =>
                        dest.Email, opt => opt.MapFrom(src => src.Email))

                        .ForMember(dest =>
                        dest.FullName, opt => opt.MapFrom(src => src.FullName))
                        .ForMember(dest =>
                        dest.Username, opt => opt.MapFrom(src => src.UserName));








        }

    }
}