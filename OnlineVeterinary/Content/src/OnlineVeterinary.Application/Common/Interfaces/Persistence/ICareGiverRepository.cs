using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.CareGivers.Entities;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface ICareGiverRepository : IGenericRepository<CareGiver>
    {
        Task<List<PetDTO>> GetPetsAsync(Guid id);
    }
}