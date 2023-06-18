using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs.CareGiverDTO;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetAll
{
    public class GetAllCareGiversQueryHandler : IRequestHandler<GetAllCareGiversQuery, List<CareGiverDTO>>
    {
        private readonly ICareGiverRepository _careGiverRepository;

        public GetAllCareGiversQueryHandler(ICareGiverRepository careGiverRepository )
        {
            _careGiverRepository = careGiverRepository;
        }
        public async Task<List<CareGiverDTO>> Handle(GetAllCareGiversQuery request, CancellationToken cancellationToken)
        {
            return await _careGiverRepository.GetAll();
        }
    }
}