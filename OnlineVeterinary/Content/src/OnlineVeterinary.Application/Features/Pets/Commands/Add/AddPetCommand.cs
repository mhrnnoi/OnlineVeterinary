using System;
using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Add
{
    public record AddPetCommand(string Name, DateOnly DateOfBirth, int PetType, string CareGiverId, string CareGiverName) : IRequest<ErrorOr<string>>;
   
}
