using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Delete
{
    public record DeletePetByIdCommand(Guid Id, string CareGiverId)  : IRequest<ErrorOr<string>>;
   
}
