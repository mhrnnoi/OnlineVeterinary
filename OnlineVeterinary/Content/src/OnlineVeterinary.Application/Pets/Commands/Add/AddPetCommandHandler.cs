using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.Pet.Entities;

namespace OnlineVeterinary.Application.Pets.Commands.Add
{
    public class AddPetCommandHandler : IRequestHandler<AddPetCommand, PetDTO>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddPetCommandHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PetDTO> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            var pet = _mapper.Map<Pet>(request);
            await _petRepository.AddAsync(pet);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PetDTO>(pet);
        }
    }
}