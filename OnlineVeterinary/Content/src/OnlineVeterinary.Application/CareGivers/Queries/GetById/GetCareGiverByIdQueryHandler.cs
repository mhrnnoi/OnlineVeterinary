using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs.CareGiverDTO;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetById
{
    public class GetCareGiverByIdQueryHandler : IRequestHandler<GetCareGiverByIdQuery, CareGiverDTO>
    {
        private readonly ICareGiverRepository _careGiverRepository;

        public GetCareGiverByIdQueryHandler(ICareGiverRepository careGiverRepository )
        {
            _careGiverRepository = careGiverRepository;
        }
        public async Task<CareGiverDTO> Handle(GetCareGiverByIdQuery request, CancellationToken cancellationToken)
        {
            return await _careGiverRepository.GetById(request.Id);
        }
    }
}