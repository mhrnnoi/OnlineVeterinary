// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;

// namespace OnlineVeterinary.Application.CareGivers.Queries.GetPets
// {
//     public class GetPetsOfCareGiverByIdQueryHandler : IRequestHandler<GetPetsOfCareGiverByIdQuery, List<PetDTO>>
//     {
//         private readonly ICareGiverRepository _careGiverRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public GetPetsOfCareGiverByIdQueryHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _careGiverRepository = careGiverRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<List<PetDTO>> Handle(GetPetsOfCareGiverByIdQuery request, CancellationToken cancellationToken)
//         {
//             var pets = await _careGiverRepository.GetPetsAsync(request.Id);
//             var petsDTO = _mapper.Map<List<PetDTO>>(pets);
//             await _unitOfWork.SaveChangesAsync();
//             return petsDTO;
//         }
//     }
// }