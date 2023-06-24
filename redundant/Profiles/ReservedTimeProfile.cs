using System;
using AutoMapper;
using OnlineVeterinary.Models.DTOs.OutGoing;
using OnlineVeterinary.Models.Identity;

namespace OnlineVeterinary.Profiles
{
    public class ReservedTimeProfile : Profile
    {
        public ReservedTimeProfile()
        {
            CreateMap<ReservedTimes, ReservedTimesDTO>()
            .ForMember(dest =>
            dest.CareGiverUserName, opt => opt.MapFrom(src => src.CareGiverUserName))
            .ForMember(dest =>
            dest.DrUserName, opt => opt.MapFrom(src => src.DrUserName))
            .ForMember(dest =>
            dest.PetUserName, opt => opt.MapFrom(src => src.PetUserName))
            .ForMember(dest =>
            dest.ReservedTime, opt => opt.MapFrom(src => src.ReservedTime))
            .ForMember(dest =>
            dest.Code, opt => opt.MapFrom(src => src.Code));


        }
    }
}
