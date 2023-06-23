using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class PetRepository : IPetRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<Pet> _petDbSet;

        public PetRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _petDbSet = _context.Set<Pet>();
        }

        public void Add(Pet entity)
        {
            _petDbSet.Add(entity);
        }

        public void Remove(Pet entity)
        {

            _petDbSet.Remove(entity);
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await _petDbSet.ToListAsync();

        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await _petDbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        

        public void Update(Pet entity)
        {
            _petDbSet.Update(entity);
        }

      

       

      

       
    }
}