using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Domain.Pet.Entities;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Add
{
    public class AddPetCommandHandler : IRequestHandler<AddPetCommand, ErrorOr<string>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public AddPetCommandHandler(
            IPetRepository petRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<string>> Handle(
            AddPetCommand request,
            CancellationToken cancellationToken)
        {
            Guid id = (request.CareGiverId);

            var user = await _userRepository.GetByIdAsync(id);
            if (user is null )
            {
                return Error.NotFound(description : "you have invalid Id or this user is not exist any more");
            }

           
            var pet = _mapper.Map<Pet>(request);
            _petRepository.Add(pet);
            await _unitOfWork.SaveChangesAsync();
            return "pet Added succesfuly";
        }


    }
}