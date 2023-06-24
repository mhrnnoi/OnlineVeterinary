using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Common.Services;

namespace OnlineVeterinary.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<string>>
    {

        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ChangePasswordCommandHandler(IUserRepository userRepository, IMapper mapper, IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId =  StringToGuidConverter.ConvertToGuid(request.Id);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null )
            {
                return Error.NotFound();
            }
            user.Password = request.NewPassword;

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return "Password changed succesfuly";



        }
    }
}