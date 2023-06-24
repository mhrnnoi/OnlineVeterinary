using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<Reservation> _reservationDbSet;

        public ReservationRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _reservationDbSet = _context.Set<Reservation>();
        }

        public void Add(Reservation entity)
        {
            _reservationDbSet.Add(entity);
        }


        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _reservationDbSet.ToListAsync();
        }



        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _reservationDbSet.SingleOrDefaultAsync(x => x.Id == id);
         
            
        }

        public void Remove(Reservation entity)
        {
           

            _reservationDbSet.Remove(entity);
        }

        public void Update(Reservation entity)
        {
            _reservationDbSet.Update(entity);
        }

      
    }
}