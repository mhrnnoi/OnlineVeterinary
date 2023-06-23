// using System;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;
// using OnlineVeterinary.Domain.CareGivers.Entities;
// using OnlineVeterinary.Domain.Reservation.Entities;

// namespace OnlineVeterinary.Application.Reservations.Commands.Update
// {
//     public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, ReservationDTO>
//     {
//         private readonly IReservationRepository _reservationRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public UpdateReservationCommandHandler(IReservationRepository reservationRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _reservationRepository = reservationRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<ReservationDTO> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
//         {
//             var reservation = _mapper.Map<Reservation>(request);
//             await _reservationRepository.UpdateAsync(reservation);
//             await _unitOfWork.SaveChangesAsync();

//             return _mapper.Map<ReservationDTO>(request);
//         }
//     }
// }
