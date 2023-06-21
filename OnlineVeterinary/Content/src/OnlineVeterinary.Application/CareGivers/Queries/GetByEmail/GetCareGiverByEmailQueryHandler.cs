using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetByEmail
{
    public class GetCareGiverByEmailQueryHandler : IRequestHandler<GetCareGiverByEmailQuery, ErrorOr<CareGiverDTO>>
    {

       
        private readonly ICareGiverRepository _careGiverRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCareGiverByEmailQueryHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _careGiverRepository = careGiverRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<CareGiverDTO>> Handle(GetCareGiverByEmailQuery request, CancellationToken cancellationToken)
        {
           var careGiverDto = await _careGiverRepository.GetByEmailAsync(request.email);
           return _mapper.Map<CareGiverDTO>(careGiverDto);
        }
    }
}