using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Features.Auth.Commands.Delete
{
    

    public class DeleteMyAccountCommandHandler : IRequestHandler<DeleteMyAccountCommand, ErrorOr<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;


        public DeleteMyAccountCommandHandler(IMapper mapper,
                                             IUnitOfWork unitOfWork,
                                             IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<string>> Handle(DeleteMyAccountCommand request,
                                                  CancellationToken cancellationToken)
        {
            Guid id = Guid.Parse(request.Id);

            var user = await _userRepository.GetByIdAsync(id);
            if (user is null )
            {
                return Error.NotFound(description : "you have invalid Id or this user is not exist any more");
            }
            _userRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync();



            return "Deleted successfully";

        }
    }
}

