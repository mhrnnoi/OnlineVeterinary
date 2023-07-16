using System;
using OnlineVeterinary.Domain.Pet.Enums;

namespace OnlineVeterinary.Application.Features.DTOs
{
    public record PetDTO(
        Guid Id,
        Guid CareGiverId,
        string Name,
        DateTime DateOfBirth,
        string PetType,
        string CareGiverLastName);
    
}
