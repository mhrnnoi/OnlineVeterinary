using ErrorOr;
using System.Linq;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetPets
{
    public class GetPetsOfCareGiverByIdQueryHandler : IRequestHandler<GetPetsOfCareGiverByIdQuery, ErrorOr<List<PetDTO>>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetPetsOfCareGiverByIdQueryHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<List<PetDTO>>> Handle(GetPetsOfCareGiverByIdQuery request, CancellationToken cancellationToken)
        {
            var pets = await _petRepository.GetAllAsync();
            var myPets = pets.Where(a=> a.CareGiverId == request.Id);
            if (myPets.Count() < 1)
            {
                return Error.NotFound();
            }
            var petsDTO = _mapper.Map<ErrorOr<List<PetDTO>>>(myPets);
            return petsDTO;
        }
    }
}