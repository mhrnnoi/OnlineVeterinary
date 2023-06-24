using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Features.Auth.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, ErrorOr<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeEmailCommandHandler(IUserRepository userRepository,
                                        IUnitOfWork unitOfWork,
                                        IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ErrorOr<string>> Handle(ChangeEmailCommand request,
                                                  CancellationToken cancellationToken)
        {
            var userid = Guid.Parse(request.Id);

            var user = await _userRepository.GetByIdAsync(userid);

            if (user is null)
            {
                return Error.NotFound("you have invalid Id or this user is not exist any more");
            }
            var isExist = await _userRepository.GetByEmailAsync(request.NewEmail);
            if (isExist is not null)
            {
                return Error.Failure(description : "this email is already exist ");
            }
            user.Email = request.NewEmail;

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return "email changed succesfuly";
        }
    }
}