using ErrorOr;
using System.Linq;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.DTOs;
using OnlineVeterinary.Application.Common.Services;

namespace OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets
{
    public class GetMyPetsQueryHandler : IRequestHandler<GetMyPetsQuery, ErrorOr<List<PetDTO>>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetMyPetsQueryHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<List<PetDTO>>> Handle(GetMyPetsQuery request, CancellationToken cancellationToken)
        {
            var pets = await _petRepository.GetAllAsync();
            var myGuidId = StringToGuidConverter.ConvertToGuid(request.Id);
            var myPets = pets.Where(a => a.CareGiverId == myGuidId);
            if (myPets.Count() < 1)
            {
                return Error.NotFound();
            }
            var petsDTO = _mapper.Map<ErrorOr<List<PetDTO>>>(myPets);
            return petsDTO;
        }
    }
}