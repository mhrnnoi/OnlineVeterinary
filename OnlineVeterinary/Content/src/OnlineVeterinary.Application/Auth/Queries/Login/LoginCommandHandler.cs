using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;

namespace OnlineVeterinary.Application.Auth.Queries.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator, IJwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<ErrorOr<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {


            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || user.Password != request.Password)
            {
                return Error.Failure(Error.Failure().Code, "email or password incorrect");
            }

            
            var token = _jwtGenerator.GenerateToken(user);
            var authResult = _mapper.Map<AuthResult>((user, token));


            return authResult;



        }
    }
}