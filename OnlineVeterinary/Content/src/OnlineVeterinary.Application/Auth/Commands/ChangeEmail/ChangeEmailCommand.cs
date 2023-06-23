using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Auth.Common;

namespace OnlineVeterinary.Application.Auth.Commands.ChangeEmail
{
    public record ChangeEmailCommand(string NewEmail, string Id) : IRequest<ErrorOr<string>>;
    
}