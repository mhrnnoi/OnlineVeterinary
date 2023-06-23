using System;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Pets.Commands.Add
{
    public record AddPetCommand(string Name, DateOnly DateOfBirth, int PetType, string CareGiverId, string CareGiverName) : IRequest<ErrorOr<string>>;
   
}
