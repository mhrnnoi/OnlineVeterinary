using System;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Application.Reservations.Commands.Common;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Doctor.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;

namespace OnlineVeterinary.Application.Reservations.Commands.Add
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

            if (doctor is null || pet is null || pet.CareGiverId != request.CareGiverId)
            {
                return Error.NotFound();
            }

            var allReservations = await _reservationRepository.GetAllAsync();
            var doctorReservations = allReservations.Where(a => a.DoctorId == doctor.Id);
            if (doctorReservations.Count() < 1)
            {
                if (_dateTimeProvider.Utc.AddMinutes(60).TimeOfDay >= WorkTime.Start.TimeOfDay && _dateTimeProvider.Utc.AddMinutes(60).TimeOfDay <= WorkTime.End.AddMinutes(-30).TimeOfDay)
                {
                    Reservation reservationResult = NewMethod(request, doctor, pet);
                    _reservationRepository.Add(reservationResult);
                    await _unitOfWork.SaveChangesAsync();
                    return _mapper.Map<ReservationDTO>(reservationResult);
                }
                else
                {
                    Reservation reservation2 = NewMethod1(request, doctor, pet);
                    _reservationRepository.Add(reservation2);
                    await _unitOfWork.SaveChangesAsync();

                    return _mapper.Map<ReservationDTO>(reservation2);


                }
            }
            else
            {
                var lastReserved = allReservations.Where(a => a.DoctorId == doctor.Id).OrderBy(a => a.DateOfReservation).LastOrDefault();

                if (lastReserved != null && lastReserved.DateOfReservation.AddMinutes(30).TimeOfDay <= WorkTime.End.AddMinutes(-30).TimeOfDay)
                {
                    Reservation reservation3 = NewMethod2(request, doctor, pet, lastReserved);
                    _reservationRepository.Add(reservation3);
                    await _unitOfWork.SaveChangesAsync();
                    return _mapper.Map<ReservationDTO>(reservation3);

                }
                else
                {
                    Reservation reservation32 = NewMethod3(request, doctor, pet, lastReserved);
                    _reservationRepository.Add(reservation32);
                    await _unitOfWork.SaveChangesAsync();
                    return _mapper.Map<ReservationDTO>(reservation32);


                }
            }


        }

        private static Reservation NewMethod3(AddReservationCommand request, Doctor? doctor, Pet? pet, Reservation? lastReserved)
        {
            var LastTime = lastReserved.DateOfReservation;
            var tomarow = LastTime.AddDays(1);
            var reservedTime = new DateTime(tomarow.Year, tomarow.Month, tomarow.Day, 07, 0, 0);
            var reservation32 = new Reservation()

            {
                DateOfReservation = reservedTime,
                CareGiverId = request.CareGiverId,
                DoctorId = request.DoctorId,
                PetId = request.PetId,
                DrLastName = doctor.LastName,
                PetName = pet.Name,
                CareGiverLastName = pet.CareGiverName

            };
            return reservation32;
        }

        private static Reservation NewMethod2(AddReservationCommand request, Doctor? doctor, Pet? pet, Reservation? lastReserved)
        {
            var reservedTime = lastReserved.DateOfReservation.AddMinutes(30);
            var reservation3 = new Reservation()

            {
                DateOfReservation = reservedTime,
                CareGiverId = request.CareGiverId,
                DoctorId = request.DoctorId,
                PetId = request.PetId,
                DrLastName = doctor.LastName,
                PetName = pet.Name,
                CareGiverLastName = pet.CareGiverName


            };
            return reservation3;
        }

        private Reservation NewMethod1(AddReservationCommand request, Doctor? doctor, Pet? pet)
        {
            var tomarow = _dateTimeProvider.Utc.AddDays(1);

            var reservedTime = new DateTime(tomarow.Year, tomarow.Month, tomarow.Day, 07, 0, 0);
            var reservation2 = new Reservation()

            {
                DateOfReservation = reservedTime,
                CareGiverId = request.CareGiverId,
                DoctorId = request.DoctorId,
                PetId = request.PetId,
                DrLastName = doctor.LastName,
                PetName = pet.Name,
                CareGiverLastName = pet.CareGiverName



            };
            return reservation2;
        }

        private Reservation NewMethod(AddReservationCommand request, Doctor? doctor, Pet? pet)
        {
            var reservedTime = _dateTimeProvider.Utc.AddHours(1);

            var reservationResult = new Reservation()
            {
                DateOfReservation = reservedTime,
                CareGiverId = request.CareGiverId,
                DoctorId = request.DoctorId,
                PetId = request.PetId,
                DrLastName = doctor.LastName,
                PetName = pet.Name,
                CareGiverLastName = pet.CareGiverName

            };
            return reservationResult;
        }
    }
}
