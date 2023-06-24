using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;

namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, ErrorOr<DoctorDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetDoctorByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<DoctorDTO>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user is null || user.Role.ToLower() != "doctor")
            {
                return Error.NotFound();
            }

            return _mapper.Map<DoctorDTO>(user);
        }
    }
}