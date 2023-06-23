using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;


namespace OnlineVeterinary.Application.Doctors.Queries.GetAll
{
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, ErrorOr<List<DoctorDTO>>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<DoctorDTO>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.GetAllAsync();
            var doctorsDTO = _mapper.Map<List<DoctorDTO>>(doctors);

            return doctorsDTO;
        }
    }
}