using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Features.Reservations.Commands.DeleteById
{
    public class DeleteReservationByIdCommandHandler : IRequestHandler<DeleteReservationByIdCommand, ErrorOr<string>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationByIdCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<string>> Handle(DeleteReservationByIdCommand request, CancellationToken cancellationToken)
        {
            
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);
            if (reservation is null || (reservation.DoctorId != request.Id && reservation.CareGiverId != request.Id ) )
            {
                return Error.NotFound();
            }
            _reservationRepository.Remove(reservation);
            await _unitOfWork.SaveChangesAsync();

            return "Deleted Succesfuly";
        }
    }
}