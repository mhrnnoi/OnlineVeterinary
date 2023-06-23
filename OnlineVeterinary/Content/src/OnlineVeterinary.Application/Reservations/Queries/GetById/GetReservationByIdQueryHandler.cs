// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;

// namespace OnlineVeterinary.Application.Reservations.Queries.GetById
// {
//     public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDTO>
//     {
//         private readonly IReservationRepository _reservationRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public GetReservationByIdQueryHandler(IReservationRepository reservationRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _reservationRepository = reservationRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<ReservationDTO> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
//         {
//             var reservation = await _reservationRepository.GetByIdAsync(request.Id);
//                         await _unitOfWork.SaveChangesAsync();

//            return  _mapper.Map<ReservationDTO>(reservation);
//         }
//     }
// }