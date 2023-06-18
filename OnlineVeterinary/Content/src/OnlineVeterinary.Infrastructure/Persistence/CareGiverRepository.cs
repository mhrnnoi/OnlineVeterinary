using System;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class CareGiverRepository : ICareGiverRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<CareGiver> _careGiverDbSet;
        private readonly DbSet<Pet> _petDbSet;

        public CareGiverRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _careGiverDbSet = _context.Set<CareGiver>();
            _petDbSet = _context.Set<Pet>();
        }

        public async Task AddAsync(CareGiver entity)
        {
            await _careGiverDbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var careGiver = await GetByIdAsync(id);
            _context.Entry(careGiver).State = EntityState.Detached;

            _careGiverDbSet.Remove(careGiver);
        }



        public async Task<List<CareGiver>> GetAllAsync()
        {
            return await _careGiverDbSet.ToListAsync();
        }



        public async Task<CareGiver> GetByIdAsync(Guid id)
        {
            var careGiver = await _careGiverDbSet.SingleOrDefaultAsync(x => x.Id == id);
            if (careGiver == null)
            {
                throw new Exception("careGiver with this Id is not exist");
            }
            return careGiver;
        }

        public async Task<List<Pet>> GetPetsAsync(Guid id)
        {
            var pets = await _petDbSet.Where(x => x.CareGiverId == id).ToListAsync();
            return pets;
        }

        public async Task UpdateAsync(CareGiver entity)
        {
            var oldCareGiver = await GetByIdAsync(entity.Id);
            _context.Entry(oldCareGiver).State = EntityState.Detached;

            await DeleteAsync(oldCareGiver.Id);
            _context.Entry(oldCareGiver).State = EntityState.Detached;

            await AddAsync(entity);
        }
    }
}
