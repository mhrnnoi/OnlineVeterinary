        using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

namespace OnlineVeterinary.Contracts.Pets.Request
{
    public record AddPetRequest(string Name,
                                DateTime DateOfBirth,
                                int PetType);


}