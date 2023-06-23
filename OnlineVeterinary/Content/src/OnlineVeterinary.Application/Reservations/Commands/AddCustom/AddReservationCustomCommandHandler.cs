// using System;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;
// using OnlineVeterinary.Domain.CareGivers.Entities;
// using OnlineVeterinary.Domain.Reservation.Entities;

// namespace OnlineVeterinary.Application.Reservations.Commands.AddCustom
// {
//     public class AddReservationCustomCommandHandler : IRequestHandler<AddReservationCustomCommand, ReservationDTO>
//     {
//         private readonly IReservationRepository _reservationRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public AddReservationCustomCommandHandler(IReservationRepository reservationRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _reservationRepository = reservationRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<ReservationDTO> Handle(AddReservationCustomCommand request, CancellationToken cancellationToken)
//         {
//             var reservation = _mapper.Map<Reservation>(request);
//             await _reservationRepository.AddAsync(reservation);
//             var reservationDTO = _mapper.Map<ReservationDTO>(request);
//             await _unitOfWork.SaveChangesAsync();

//             return reservationDTO;
//         }
//     }
// }
