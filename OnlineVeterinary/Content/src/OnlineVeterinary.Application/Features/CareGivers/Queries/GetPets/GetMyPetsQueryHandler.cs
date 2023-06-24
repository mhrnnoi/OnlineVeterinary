using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.DTOs;

namespace OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets
{
    public class GetMyPetsQueryHandler : IRequestHandler<GetMyPetsQuery, ErrorOr<List<PetDTO>>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserRepository _userRepository;

        public GetMyPetsQueryHandler(
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
        public async Task<ErrorOr<List<PetDTO>>> Handle(
                                    GetMyPetsQuery request,
                                    CancellationToken cancellationToken)
        {
            var myGuidId = Guid.Parse(request.Id);
            var user = await _userRepository.GetByIdAsync(myGuidId);
            if (user is null)
            {
                return Error.NotFound("you have invalid Id or this user is not exist any more");
            }
            var pets = await _petRepository.GetAllAsync();

            var myPets = pets.Where(a => a.CareGiverId == myGuidId);
            if (myPets.Count() < 1)
            {
                return Error.NotFound();
            }
            var petsDTO = _mapper.Map<List<PetDTO>>(myPets);
            return petsDTO;
        }
    }
}