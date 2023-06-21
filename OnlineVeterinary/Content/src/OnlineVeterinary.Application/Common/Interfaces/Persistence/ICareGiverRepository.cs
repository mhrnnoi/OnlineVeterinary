using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Pet.Entities;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface ICareGiverRepository : IGenericRepository<CareGiver>
    {
        Task<List<Pet>> GetPetsAsync(Guid id);
        Task<CareGiver> GetByEmailAsync(string email);
    }
}