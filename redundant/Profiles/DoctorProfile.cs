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
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {


            CreateMap<Doctor, DoctorDTO>()
                        .ForMember(dest =>
                        dest.Email, opt => opt.MapFrom(src => src.Email))
                        .ForMember(dest =>
                        dest.Bio, opt => opt.MapFrom(src => src.Bio))
                        .ForMember(dest =>
                        dest.FullName, opt => opt.MapFrom(src => src.FullName))
                        .ForMember(dest =>
                        dest.Dislikes, opt => opt.MapFrom(src => src.Dislikes))
                        .ForMember(dest =>
                        dest.SuccesfulVisits, opt => opt.MapFrom(src => src.SuccesfulVisits))
                        .ForMember(dest =>
                        dest.Specific, opt => opt.MapFrom(src => src.Specific))
                        .ForMember(dest =>
                        dest.Location, opt => opt.MapFrom(src => src.Location))
                        .ForMember(dest =>
                        dest.Likes, opt => opt.MapFrom(src => src.Likes))
                        .ForMember(dest =>
                        dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                        .ForMember(dest =>
                        dest.Username, opt => opt.MapFrom(src => src.UserName));





        }

    }
}