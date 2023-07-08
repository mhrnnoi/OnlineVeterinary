using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.DTOs;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets
{
    public class GetMyPetsQueryHandler : IRequestHandler<GetMyPetsQuery, ErrorOr<List<PetDTO>>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private readonly IUserRepository _userRepository;

        public GetMyPetsQueryHandler(
                                    IPetRepository petRepository,
                                    IMapper mapper,
                                    IUserRepository userRepository,
                                    ICacheService cacheService)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _cacheService = cacheService;
        }
        public async Task<ErrorOr<List<PetDTO>>> Handle(
                                    GetMyPetsQuery request,
                                    CancellationToken cancellationToken)
        {
            var myGuidId = Guid.Parse(request.Id);

            var user = await _userRepository.GetByIdAsync(myGuidId);
            if (user is null)
            {
                return Error.NotFound(description: "you have invalid Id or this user is not exist any more");
            }






            var petsDTO = _cacheService.GetData<List<PetDTO>>($"{myGuidId} pets");
            if (petsDTO != null && petsDTO.Count() > 0)
            {
                return petsDTO;
            }


            var pets = await _petRepository.GetAllAsync();
            var myPets = pets.Where(a => a.CareGiverId == myGuidId);
            petsDTO = _mapper.Map<List<PetDTO>>(myPets);
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<PetDTO>>($"{myGuidId} pets", petsDTO, expiryTime);

            return petsDTO;
        }
    }
}