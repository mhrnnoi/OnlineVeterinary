using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;


namespace OnlineVeterinary.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<string>>
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ChangePasswordCommandHandler(IUserRepository userRepository,
                                            IMapper mapper,
                                            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<string>> Handle(ChangePasswordCommand request,
                                                  CancellationToken cancellationToken)
        {
            var userId =  Guid.Parse(request.Id);

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