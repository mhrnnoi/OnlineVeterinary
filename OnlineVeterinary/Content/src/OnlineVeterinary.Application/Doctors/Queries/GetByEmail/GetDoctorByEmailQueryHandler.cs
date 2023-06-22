using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.Doctor.Entities;

namespace OnlineVeterinary.Application.Doctors.Queries.GetByEmail
{
    public class GetDoctorByEmailQueryHandler : IRequestHandler<GetDoctorByEmailQuery, ErrorOr<User>>
    {

       
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetDoctorByEmailQueryHandler(IDoctorRepository doctorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<User>> Handle(GetDoctorByEmailQuery request, CancellationToken cancellationToken)
        {
           var doctor = await _doctorRepository.GetByEmailAsync(request.email);
           return _mapper.Map<User>(doctor);
        }
    }
}