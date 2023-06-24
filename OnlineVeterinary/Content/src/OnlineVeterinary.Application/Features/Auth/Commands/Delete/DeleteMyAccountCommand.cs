using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Features.Auth.Commands.Delete
{
    public record DeleteMyAccountCommand(string Id) : IRequest<ErrorOr<string>>;
   
}