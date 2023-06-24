using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Common;

namespace OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, ErrorOr<List<ReservationDTO>>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public GetAllReservationsQueryHandler(IReservationRepository reservationRepository,
                                              IMapper mapper,
                                              IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<List<ReservationDTO>>> Handle(GetAllReservationsQuery request,
                                                                CancellationToken cancellationToken)
        {
            var myGuidId = Guid.Parse(request.Id);

            var user = await _userRepository.GetByIdAsync(myGuidId);
            if (user is null)
            {
                return Error.NotFound(description : "you have invalid Id or this user is not exist any more");
            }

            var reservations = await _reservationRepository.GetAllAsync();
            var myReservations = request.Role.ToLower() switch
            {
                "doctor" => reservations.Where(a => a.DoctorId == myGuidId),
                "caregiver" => reservations.Where(a => a.CareGiverId == myGuidId),
                _ => reservations
            };

            var myReservationsDTO = _mapper.Map<List<ReservationDTO>>(myReservations);

            return myReservationsDTO;
        }
    }
}