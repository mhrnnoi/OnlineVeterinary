using System;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Delete
{
    public class DeletePetByIdCommandHandler : IRequestHandler<DeletePetByIdCommand, ErrorOr<string>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePetByIdCommandHandler(
                                IPetRepository petRepository,
                                IMapper mapper,
                                IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<string>> Handle(
                                        DeletePetByIdCommand request,
                                        CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetByIdAsync(request.Id);
            var myGuidId = Guid.Parse(request.CareGiverId);

            if (pet is null || pet.CareGiverId != myGuidId)
            {
                return Error.NotFound();
            }
            _petRepository.Remove(pet);
            await _unitOfWork.SaveChangesAsync();
            return "Deleted succesfuly";
        }
    }
}
