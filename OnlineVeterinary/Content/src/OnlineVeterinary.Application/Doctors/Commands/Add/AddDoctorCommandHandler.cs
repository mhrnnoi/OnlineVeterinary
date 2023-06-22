using System;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.Doctor.Entities;

namespace OnlineVeterinary.Application.Doctors.Commands.Add
{
    public class AddDoctorCommandHandler : IRequestHandler<AddDoctorCommand, ErrorOr<User>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<User>> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = _mapper.Map<Doctor>(request);
            await _doctorRepository.AddAsync(doctor);
            var doctorDTO = _mapper.Map<User>(request);
            await _unitOfWork.SaveChangesAsync();

            return doctorDTO;
        }
    }
}
