using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
    {
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;


        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtGenerator.GenerateToken(user);
            var authResult = _mapper.Map<AuthResult>((user, token));

            return authResult;






        }
    }
}