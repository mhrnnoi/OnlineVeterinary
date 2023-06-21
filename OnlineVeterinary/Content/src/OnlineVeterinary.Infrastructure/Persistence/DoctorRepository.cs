using System;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Doctor.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<Doctor> _doctorDbSet;

        public DoctorRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _doctorDbSet = _context.Set<Doctor>();
        }

        public async Task AddAsync(Doctor entity)
        {
            await _doctorDbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var doctor = await GetByIdAsync(id);
            _context.Entry(doctor).State = EntityState.Detached;

            _doctorDbSet.Remove(doctor);
        }



        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _doctorDbSet.ToListAsync();
        }

        public Task<Doctor> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> GetByIdAsync(Guid id)
        {
            var Doctor = await _doctorDbSet.SingleOrDefaultAsync(x => x.Id == id);
            if (Doctor == null)
            {
                throw new Exception("Doctor with this Id is not exist");
            }
            return Doctor;
        }

        // public async Task<List<Pet>> GetPetsAsync(Guid id)
        // {
        //     var pets = await _petDbSet.Where(x => x.DoctorId == id).ToListAsync();
        //     return pets;
        // }
        // public async Task<List<CareGiver>> GetCareGiversAsync(Guid id)
        // {
        //     var pets = await _petDbSet.Where(x => x.DoctorId == id).ToListAsync();
        //     return pets;
        // }
        // public async Task<List<Reservation>> GetReservationsAsync(Guid id)
        // {
        //     var pets = await _petDbSet.Where(x => x.DoctorId == id).ToListAsync();
        //     return pets;
        // }


        public async Task UpdateAsync(Doctor entity)
        {
            var oldDoctor = await GetByIdAsync(entity.Id);
            _context.Entry(oldDoctor).State = EntityState.Detached;

            await DeleteAsync(oldDoctor.Id);
            _context.Entry(oldDoctor).State = EntityState.Detached;

            await AddAsync(entity);
        }
    }
}
