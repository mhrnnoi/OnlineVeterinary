using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class PetRepository : IPetRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private ICareGiverRepository _careGiverRepository;
        private readonly DbSet<Pet> _petDbSet;

        public PetRepository(AppDbContext context, IMapper mapper, ICareGiverRepository careGiverRepository)
        {
            _context = context;
            _mapper = mapper;
            _careGiverRepository = careGiverRepository;
            _petDbSet = _context.Set<Pet>();
        }

        public async Task AddAsync(Pet entity)
        {
            await _petDbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var pet = await GetByIdAsync(id);
            _context.Entry(pet).State = EntityState.Detached;

            _petDbSet.Remove(pet);
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await _petDbSet.ToListAsync();

        }

        public async Task<Pet> GetByIdAsync(Guid id)
        {
            var pet = await _petDbSet.SingleOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                throw new Exception("pet with this Id is not exist");
            }
            return pet;
        }

        public async Task<CareGiver> GetCareGiverOfPet(Guid id)
        {
            var pet = await GetByIdAsync(id);
            var careGiver = await _careGiverRepository.GetByIdAsync(pet.Id);
            return careGiver;
        }

        public async Task UpdateAsync(Pet entity)
        {
            var oldPet = await GetByIdAsync(entity.Id);
            _context.Entry(oldPet).State = EntityState.Detached;

            await DeleteAsync(oldPet.Id);
            _context.Entry(oldPet).State = EntityState.Detached;

            await AddAsync(entity);
        }
    }
}