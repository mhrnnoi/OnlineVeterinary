using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Doctors.Commands.Update
{
    public record UpdateDoctorCommand(Guid Id, string FirstName, string LastName, string Email, string Password) : IRequest<DoctorDTO>;
    
}
