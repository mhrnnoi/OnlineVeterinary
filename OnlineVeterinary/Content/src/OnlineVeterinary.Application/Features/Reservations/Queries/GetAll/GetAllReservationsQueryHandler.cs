using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Services;
using OnlineVeterinary.Application.Features.Common;

namespace OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, ErrorOr<List<ReservationDTO>>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetAllReservationsQueryHandler(IReservationRepository reservationRepository,
                                              IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<ReservationDTO>>> Handle(GetAllReservationsQuery request,
                                                                CancellationToken cancellationToken)
        {
            var myGuidId = StringToGuidConverter.ConvertToGuid(request.Id);
            var reservations = await _reservationRepository.GetAllAsync();
            var myReservations  = request.Role.ToLower() switch
            {
                "doctor" => reservations.Where(a => a.DoctorId == myGuidId),
                "caregiver" => reservations.Where(a => a.CareGiverId == myGuidId),
                 _ => reservations
            };
            if (myReservations.Count() < 1)
            {
                return Error.NotFound();
            }
            var myReservationsDTO = _mapper.Map<List<ReservationDTO>>(myReservations);

            return myReservationsDTO ;
        }
    }
}