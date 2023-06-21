using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Doctors.Queries.GetByEmail
{
    public class GetDoctorByEmailQueryHandler : IRequestHandler<GetDoctorByEmailQuery, ErrorOr<DoctorDTO>>
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
        public async Task<ErrorOr<DoctorDTO>> Handle(GetDoctorByEmailQuery request, CancellationToken cancellationToken)
        {
           var doctorDto = await _doctorRepository.GetByEmailAsync(request.email);
           return _mapper.Map<DoctorDTO>(doctorDto);
        }
    }
}