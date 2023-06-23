using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Auth.Common;

namespace OnlineVeterinary.Application.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(string Id, string NewPassword) : IRequest<ErrorOr<string>>;

}