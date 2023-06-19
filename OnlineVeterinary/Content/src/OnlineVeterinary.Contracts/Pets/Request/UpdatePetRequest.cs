using System;

namespace OnlineVeterinary.Contracts.Pets.Request
{
    public record UpdatePetRequest(Guid Id, string Name,
        DateOnly DateOfBirth,
        int PetType);
    
}
