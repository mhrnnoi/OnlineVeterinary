// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MapsterMapper;
// using Microsoft.EntityFrameworkCore;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Domain.Reservation.Entities;
// using OnlineVeterinary.Infrastructure.Persistence.DataContext;

// namespace OnlineVeterinary.Infrastructure.Persistence
// {
//     public class ReservationRepository : IReservationRepository
//     {
//          private readonly AppDbContext _context;
//         private readonly IMapper _mapper;
//         private readonly DbSet<Reservation> _reservationDbSet;

//         public ReservationRepository(AppDbContext context, IMapper mapper)
//         {
//             _context = context;
//             _mapper = mapper;
//             _reservationDbSet = _context.Set<Reservation>();
//         }
//          public async Task AddAsync(Reservation entity)
//         {
//             await _reservationDbSet.AddAsync(entity);
//         }

//         public async Task DeleteAsync(Guid id)
//         {
//             var reservation = await GetByIdAsync(id);
//             _context.Entry(reservation).State = EntityState.Detached;

//             _reservationDbSet.Remove(reservation);
//         }



//         public async Task<List<Reservation>> GetAllAsync()
//         {
//             return await _reservationDbSet.ToListAsync();
//         }



//         public async Task<Reservation> GetByIdAsync(Guid id)
//         {
//             var reservation = await _reservationDbSet.SingleOrDefaultAsync(x => x.Id == id);
//             if (reservation == null)
//             {
//                 throw new Exception("reservation with this Id is not exist");
//             }
//             return reservation;
//         }

      


//         public async Task UpdateAsync(Reservation entity)
//         {
//             var oldreservation = await GetByIdAsync(entity.Id);
//             _context.Entry(oldreservation).State = EntityState.Detached;

//             await DeleteAsync(oldreservation.Id);
//             _context.Entry(oldreservation).State = EntityState.Detached;

//             await AddAsync(entity);
//         }
//     }
// }