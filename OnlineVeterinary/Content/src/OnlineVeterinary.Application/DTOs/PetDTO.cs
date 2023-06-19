using System;
using OnlineVeterinary.Domain.Pet.Enums;

namespace OnlineVeterinary.Application.DTOs
{
    public record PetDTO(string Name, DateOnly DateOfBirth, string PetType);
    
}
