// using System;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;
// using OnlineVeterinary.Domain.Pet.Entities;

// namespace OnlineVeterinary.Application.Pets.Commands.Update
// {
//     public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, PetDTO>
//     {
//         private readonly IPetRepository _petRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public UpdatePetCommandHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _petRepository = petRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<PetDTO> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
//         {
//             var pet = _mapper.Map<Pet>(request);
//             await _petRepository.UpdateAsync(pet);
//             return _mapper.Map<PetDTO>(pet);
//         }
//     }
// }
