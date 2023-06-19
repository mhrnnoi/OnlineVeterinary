using System;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Pets.Commands.Delete
{
    public class DeletePetByIdCommandHandler : IRequestHandler<DeletePetByIdCommand, string>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePetByIdCommandHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(DeletePetByIdCommand request, CancellationToken cancellationToken)
        {
            await _petRepository.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return "Deleted succesfuly";
        }
    }
}
