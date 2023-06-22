using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.CareGivers.Commands.DeleteById
{
    public class DeleteCareGiverByIdCommandHandler : IRequestHandler<DeleteCareGiverByIdCommand, ErrorOr<string>>
    {
        private readonly ICareGiverRepository _careGiverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCareGiverByIdCommandHandler(ICareGiverRepository careGiverRepository, IUnitOfWork unitOfWork)
        {
            _careGiverRepository = careGiverRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<string>> Handle(DeleteCareGiverByIdCommand request, CancellationToken cancellationToken)
        {
            await _careGiverRepository.DeleteAsync(request.Id);
                        await _unitOfWork.SaveChangesAsync();

            return "Deleted Succesfuly";
        }
    }
}