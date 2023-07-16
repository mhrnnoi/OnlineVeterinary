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
        private readonly ICacheService _cacheService;

        public GetAllDoctorsQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<ErrorOr<List<DoctorDTO>>> Handle(
            GetAllDoctorsQuery request,
            CancellationToken cancellationToken)
        {
            var doctorsDTO = _cacheService.GetData<List<DoctorDTO>>("doctors");
            if (doctorsDTO != null && doctorsDTO.Count() > 0)
            {
                return doctorsDTO;
            }
            var users = await _userRepository.GetAllAsync();
            var doctors = users.Where(a => a.Role.ToLower() == "doctor");
            doctorsDTO = _mapper.Map<List<DoctorDTO>>(doctors);
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<DoctorDTO>>("doctors", doctorsDTO, expiryTime);

            return doctorsDTO;
        }
    }
}