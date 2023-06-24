using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Services;

namespace OnlineVeterinary.Application.Features.Reservations.Commands.DeleteById
{
    public class DeleteReservationByIdCommandHandler : IRequestHandler<DeleteReservationByIdCommand, ErrorOr<string>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationByIdCommandHandler(IReservationRepository reservationRepository,
                                                   IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<string>> Handle(DeleteReservationByIdCommand request,
                                                  CancellationToken cancellationToken)
        {
            var userId = StringToGuidConverter.ConvertToGuid(request.userId);
            var reservations = await _reservationRepository.GetAllAsync();
            var userReservations = request.Role.ToLower() switch 
            {
                "doctor" =>  reservations.Where(a=> a.DoctorId == userId),
                "caregiver" =>  reservations.Where(a=> a.CareGiverId == userId),
                _ => reservations
                
            };
            
            var reservation = userReservations.SingleOrDefault(a=> a.Id == request.Id);
            if (reservation is null  )
            {
                return Error.NotFound();
            }
            _reservationRepository.Remove(reservation);
            await _unitOfWork.SaveChangesAsync();

            return "Deleted Succesfuly";
        }
    }
}