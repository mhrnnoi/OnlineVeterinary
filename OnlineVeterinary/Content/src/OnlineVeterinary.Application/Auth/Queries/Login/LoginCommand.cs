using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Auth.Common;

namespace OnlineVeterinary.Application.Auth.Queries.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<ErrorOr<AuthResult>>;
    
}