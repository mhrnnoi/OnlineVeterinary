using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Features.Auth.Common;

namespace OnlineVeterinary.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(string FirstName,
                                  string LastName,
                                  string Email,
                                  string Password,
                                  int Role) : IRequest<ErrorOr<AuthResult>>;

   
}