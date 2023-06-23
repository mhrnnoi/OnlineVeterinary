// using System;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;

// namespace OnlineVeterinary.Application.Pets.Queries.GetAll
// {
//     public class GetAllPetsQueryHandler : IRequestHandler<GetAllPetsQuery, List<PetDTO>>
//     {
//          private readonly IPetRepository _petRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public GetAllPetsQueryHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _petRepository = petRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<List<PetDTO>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
//         {
//             var pets =  await _petRepository.GetAllAsync();
//             var petsDTO = _mapper.Map<List<PetDTO>>(pets);
//             await _unitOfWork.SaveChangesAsync();
//             return petsDTO;
//         }
//     }
// }
