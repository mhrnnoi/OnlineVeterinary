// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using ErrorOr;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;

// namespace OnlineVeterinary.Application.CareGivers.Queries.GetById
// {
//     public class GetCareGiverByIdQueryHandler : IRequestHandler<GetCareGiverByIdQuery, ErrorOr<CareGiverDTO>>
//     {
//         private readonly ICareGiverRepository _careGiverRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public GetCareGiverByIdQueryHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _careGiverRepository = careGiverRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<ErrorOr<CareGiverDTO>> Handle(GetCareGiverByIdQuery request, CancellationToken cancellationToken)
//         {
//             var careGiver = await _careGiverRepository.GetByIdAsync(request.Id);
//                         await _unitOfWork.SaveChangesAsync();

//            return  _mapper.Map<CareGiverDTO>(careGiver);
//         }
//     }
// }