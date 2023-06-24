using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Pet.Entities;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface IPetRepository : IGenericRepository<Pet>
    {
       
    }
}