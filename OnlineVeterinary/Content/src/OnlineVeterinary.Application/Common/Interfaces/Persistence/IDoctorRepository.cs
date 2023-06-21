using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Doctor.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        // Task<List<Pet>> GetPetsAsync(Guid id);
        
        // Task<List<CareGiver>> GetCareGiversAsync(Guid id);
       
        // Task<List<Reservation>> GetReservationsAsync(Guid id);
        Task<Doctor> GetByEmailAsync(string email);

       
    }
}