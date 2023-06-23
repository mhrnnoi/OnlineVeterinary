// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;


// namespace OnlineVeterinary.Application.ReservedTimes.Queries.GetAll
// {
//     public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<ReservationDTO>>
//     {
//         private readonly IReservationRepository _reservationRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public GetAllReservationsQueryHandler(IReservationRepository reservationRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _reservationRepository = reservationRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<List<ReservationDTO>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
//         {
//             var reservations = await _reservationRepository.GetAllAsync();
//             var reservationsDTO = _mapper.Map<List<ReservationDTO>>(reservations);
//             await _unitOfWork.SaveChangesAsync();

//             return reservationsDTO;
//         }
//     }
// }