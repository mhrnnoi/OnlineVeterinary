using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Common.Services;
using OnlineVeterinary.Application.Features.Common;
using OnlineVeterinary.Domain.Doctor.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;

namespace OnlineVeterinary.Application.Features.Reservations.Commands.Add
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, ErrorOr<ReservationDTO>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPetRepository _petRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AddReservationCommandHandler(IReservationRepository reservationRepository, IMapper mapper, IUnitOfWork unitOfWork, IDoctorRepository doctorRepository, IPetRepository petRepository, IDateTimeProvider dateTimeProvider)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
            _petRepository = petRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<ErrorOr<ReservationDTO>> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            var pet = await _petRepository.GetByIdAsync(request.PetId);
            var myGuidId = StringToGuidConverter.ConvertToGuid(request.CareGiverId);
            Reservation reservation;

            
            if (doctor is null || pet is null || pet.CareGiverId != myGuidId)
            {
                return Error.NotFound();
            }

            var allReservations = await _reservationRepository.GetAllAsync();
            var doctorReservations = allReservations.Where(a => a.DoctorId == doctor.Id);
            var lastReserved = doctorReservations.OrderBy(a => a.DateOfReservation).LastOrDefault();

            var reserveDate = (lastReserved == null) ? _dateTimeProvider.UtcNow.AddMinutes(30)
                                : lastReserved.DateOfReservation.AddMinutes(30);

            if (IsInWorkingHours(reserveDate.TimeOfDay))
            {
                reservation = FormReservation(doctor, pet, myGuidId, reserveDate);
                return _mapper.Map<ReservationDTO>(reservation);
            }

            reserveDate = new DateTime(reserveDate.Year, reserveDate.Month, reserveDate.Day + 1, 07, 0, 0);
            reservation = FormReservation(doctor, pet, myGuidId, reserveDate);

            return _mapper.Map<ReservationDTO>(reservation);


        }

        private static Reservation FormReservation(Doctor doctor, Pet pet, Guid myGuidId, DateTime reserveDate)
        {
            return new Reservation()

            {
                DateOfReservation = reserveDate,
                CareGiverId = myGuidId,
                DoctorId = doctor.Id,
                PetId = pet.Id,
                DrLastName = doctor.LastName,
                PetName = pet.Name,
                CareGiverLastName = pet.CareGiverLastName


            };
        }

        private bool IsInWorkingHours(TimeSpan time)
        {
            return time >= WorkTime.Start.TimeOfDay &&
             time <= WorkTime.End.AddMinutes(-30).TimeOfDay;
        }


    }
}
