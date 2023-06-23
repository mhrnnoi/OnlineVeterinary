// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;

// namespace OnlineVeterinary.Application.Reservations.Commands.DeleteById
// {
//     public class DeleteReservationByIdCommandHandler : IRequestHandler<DeleteReservationByIdCommand, string>
//     {
//         private readonly IReservationRepository _reservationRepository;
//         private readonly IUnitOfWork _unitOfWork;

//         public DeleteReservationByIdCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
//         {
//             _reservationRepository = reservationRepository;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<string> Handle(DeleteReservationByIdCommand request, CancellationToken cancellationToken)
//         {
            
//             await _reservationRepository.DeleteAsync(request.Id);
//                         await _unitOfWork.SaveChangesAsync();

//             return "Deleted Succesfuly";
//         }
//     }
// }