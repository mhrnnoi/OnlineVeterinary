using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Doctors.Queries.GetById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, ErrorOr<DoctorDTO>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<DoctorDTO>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.Id);
            if (doctor is null)
            {
                return Error.NotFound();
            }

            return _mapper.Map<ErrorOr<DoctorDTO>>(doctor);
        }
    }
}