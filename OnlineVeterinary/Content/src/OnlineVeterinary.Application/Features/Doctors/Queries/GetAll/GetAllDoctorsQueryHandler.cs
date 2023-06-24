using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;

namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetAll
{
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, ErrorOr<List<DoctorDTO>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllDoctorsQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<DoctorDTO>>> Handle(
            GetAllDoctorsQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var doctors = users.Where(a=> a.Role.ToLower() =="doctor");
            var doctorsDTO = _mapper.Map<List<DoctorDTO>>(doctors);

            return doctorsDTO;
        }
    }
}