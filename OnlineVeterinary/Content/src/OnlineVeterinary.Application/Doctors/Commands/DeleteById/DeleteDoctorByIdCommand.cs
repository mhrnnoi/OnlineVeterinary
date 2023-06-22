using System;
using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Doctors.Commands.DeleteById
{
    public record DeleteDoctorByIdCommand(Guid Id) : IRequest<ErrorOr<string>>;
   
}
