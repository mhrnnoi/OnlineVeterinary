using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Doctors.Commands.Add
{
    public record AddDoctorCommand(string FirstName, string LastName, string Email, string Password) : IRequest<DoctorDTO>;
    
}
