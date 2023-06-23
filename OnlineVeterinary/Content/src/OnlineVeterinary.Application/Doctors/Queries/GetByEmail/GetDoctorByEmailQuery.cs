using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.Doctor.Entities;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.Doctors.Queries.GetByEmail
{
    public record GetDoctorByEmailQuery(string email) : IRequest<ErrorOr<User>>;
   
}