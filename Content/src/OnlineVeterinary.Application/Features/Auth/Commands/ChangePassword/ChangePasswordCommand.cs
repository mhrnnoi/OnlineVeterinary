using ErrorOr;
using MediatR;


namespace OnlineVeterinary.Application.Features.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(string Id, string NewPassword) : IRequest<ErrorOr<string>>;

}