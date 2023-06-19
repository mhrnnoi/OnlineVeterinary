using System;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Pets.Queries.GetCareGiver
{
    public class GetCareGiverOfPetByIdQueryHandler : IRequestHandler<GetCareGiverOfPetByIdQuery, CareGiverDTO>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCareGiverOfPetByIdQueryHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CareGiverDTO> Handle(GetCareGiverOfPetByIdQuery request, CancellationToken cancellationToken)
        {
            var careGiver = await _petRepository.GetCareGiverOfPet(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CareGiverDTO>(careGiver);
        }
    }
}
