using System;
using OnlineVeterinary.Domain.Pet.Enums;

namespace OnlineVeterinary.Application.DTOs
{
    public record PetDTO(Guid Id, string CareGiverId, string Name, DateOnly DateOfBirth, string PetType, string CareGiverName);
    
}
