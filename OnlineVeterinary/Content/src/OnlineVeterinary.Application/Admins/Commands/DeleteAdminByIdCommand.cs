using System;
using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Admins.Commands
{
    public record DeleteAdminByIdCommand(Guid Id) : IRequest<ErrorOr<string>>;
    
}
