// using System;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;

// namespace OnlineVeterinary.Application.Pets.Queries.GetPet
// {
//     public class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, PetDTO>
//     {
//         private readonly IPetRepository _petRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public GetPetByIdQueryHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _petRepository = petRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<PetDTO> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
//         {
//             var pet =  await _petRepository.GetByIdAsync(request.Id); 
//             await _unitOfWork.SaveChangesAsync();
//             return _mapper.Map<PetDTO>(pet);
//         }
//     }
// }
