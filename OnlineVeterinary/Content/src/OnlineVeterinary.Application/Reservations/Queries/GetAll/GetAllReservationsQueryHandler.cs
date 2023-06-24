using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;


namespace OnlineVeterinary.Application.ReservedTimes.Queries.GetAll
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, ErrorOr<List<ReservationDTO>>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetAllReservationsQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<ReservationDTO>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var myReservations  = request.Role.ToLower() switch
            {
                "doctor" => reservations.Where(a => a.DoctorId == request.Id),
                "caregiver" => reservations.Where(a => a.CareGiverId == request.Id),
                 _ => reservations
            };
            if (myReservations.Count() < 1)
            {
                return Error.NotFound();
            }
            var myReservationsDTO = _mapper.Map<List<ReservationDTO>>(myReservations);

            return myReservationsDTO;
        }
    }
}