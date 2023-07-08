using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;


namespace OnlineVeterinary.Application.Features.Reservations.Commands.DeleteById
{
    public class DeleteReservationByIdCommandHandler : IRequestHandler<DeleteReservationByIdCommand, ErrorOr<string>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;


        public DeleteReservationByIdCommandHandler(IReservationRepository reservationRepository,
                                                   IUnitOfWork unitOfWork,
                                                   IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<string>> Handle(DeleteReservationByIdCommand request,
                                                  CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.userId);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null )
            {
                return Error.NotFound(description : "you have invalid Id or this user is not exist any more");
            }
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
                return Error.NotFound(description : "you dont have any reservation with this id");
            }
            _reservationRepository.Remove(reservation);
            await _unitOfWork.SaveChangesAsync();

            return "Deleted successfully";
        }
    }
}