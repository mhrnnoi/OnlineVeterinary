using System;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs.CareGiverDTO;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class CareGiverRepository : ICareGiverRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CareGiverRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task Add(CareGiverDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CareGiverDTO>> GetAll()
        {
            return _mapper.Map<List<CareGiverDTO>>(await _context.Set<CareGiver>().ToListAsync());
        }

        public async Task<CareGiverDTO> GetById(Guid id)
        {
            var careGiver = await _context.Set<CareGiver>().SingleOrDefaultAsync(x => x.Id == id);
            if (careGiver == null)
            {
                throw new Exception("careGiver with this Id is not exist");
            }
            return _mapper.Map<CareGiverDTO>(careGiver);

        }

        public Task Update(CareGiverDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
