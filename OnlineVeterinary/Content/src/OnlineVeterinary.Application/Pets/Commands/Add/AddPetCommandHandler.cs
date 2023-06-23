using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.Pet.Entities;

namespace OnlineVeterinary.Application.Pets.Commands.Add
{
    public class AddPetCommandHandler : IRequestHandler<AddPetCommand, ErrorOr<string>>
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
        public async Task<ErrorOr<string>> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            var pet =  _mapper.Map<Pet>(request);
            
            _petRepository.Add(pet);
            await _unitOfWork.SaveChangesAsync();
            return "pet Added succesfuly";
        }
    }
}