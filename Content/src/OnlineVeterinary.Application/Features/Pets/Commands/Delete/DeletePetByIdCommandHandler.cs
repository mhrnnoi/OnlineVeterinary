using System;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Delete
{
    public class DeletePetByIdCommandHandler : IRequestHandler<DeletePetByIdCommand, ErrorOr<string>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;

        public DeletePetByIdCommandHandler(
                                IPetRepository petRepository,
                                IMapper mapper,
                                IUnitOfWork unitOfWork,
                                IUserRepository userRepository,
                                ICacheService cacheService)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _cacheService = cacheService;
        }
        public async Task<ErrorOr<string>> Handle(
                                        DeletePetByIdCommand request,
                                        CancellationToken cancellationToken)
        {
            var myGuidId = Guid.Parse(request.CareGiverId);

            var user = await _userRepository.GetByIdAsync(myGuidId);
            if (user is null)
            {
                return Error.NotFound(description : "you have invalid Id or this user is not exist any more");
            }
            var pet = await _petRepository.GetByIdAsync(request.Id);

            if (pet is null || pet.CareGiverId != myGuidId)
            {
                return Error.NotFound(description : "you dont have any pet with this id");
            }
            _petRepository.Remove(pet);
            await _unitOfWork.SaveChangesAsync();
            _cacheService.RemoveData($"{myGuidId} pets");
            return "Deleted successfully";
        }
    }
}
