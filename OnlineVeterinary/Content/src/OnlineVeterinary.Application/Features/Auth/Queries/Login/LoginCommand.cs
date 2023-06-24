using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Features.Auth.Common;

namespace OnlineVeterinary.Application.Features.Auth.Queries.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<ErrorOr<AuthResult>>;
    
}