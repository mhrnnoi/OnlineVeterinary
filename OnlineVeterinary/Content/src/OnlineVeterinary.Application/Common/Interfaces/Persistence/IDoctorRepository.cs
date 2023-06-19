using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Domain.Doctor.Entities;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        
    }
}