using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.CareGivers.Entities;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetByEmail
{
    public class GetCareGiverByEmailQueryHandler : IRequestHandler<GetCareGiverByEmailQuery, ErrorOr<CareGiver>>
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
        public async Task<ErrorOr<CareGiver>> Handle(GetCareGiverByEmailQuery request, CancellationToken cancellationToken)
        {
           var careGiver = await _careGiverRepository.GetByEmailAsync(request.email);
           return _mapper.Map<CareGiver>(careGiver);
        }
    }
}