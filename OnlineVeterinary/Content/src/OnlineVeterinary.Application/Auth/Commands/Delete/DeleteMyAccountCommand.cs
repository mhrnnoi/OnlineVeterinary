using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Auth.Commands.Delete
{
    public record DeleteMyAccountCommand(string Id) : IRequest<ErrorOr<string>>;
   
}