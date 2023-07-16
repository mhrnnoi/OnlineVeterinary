using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Features.Auth.Commands.ChangeEmail
{
    public record ChangeEmailCommand(string NewEmail, string Id) : IRequest<ErrorOr<string>>;
    
}