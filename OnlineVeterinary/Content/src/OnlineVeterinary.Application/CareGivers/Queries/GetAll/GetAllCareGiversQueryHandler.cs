// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;


// namespace OnlineVeterinary.Application.CareGivers.Queries.GetAll
// {
//     public class GetAllCareGiversQueryHandler : IRequestHandler<GetAllCareGiversQuery, List<CareGiverDTO>>
//     {
//         private readonly ICareGiverRepository _careGiverRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public GetAllCareGiversQueryHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _careGiverRepository = careGiverRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<List<CareGiverDTO>> Handle(GetAllCareGiversQuery request, CancellationToken cancellationToken)
//         {
//             var careGivers = await _careGiverRepository.GetAllAsync();
//             var careGiversDTO = _mapper.Map<List<CareGiverDTO>>(careGivers);
//             await _unitOfWork.SaveChangesAsync();

//             return careGiversDTO;
//         }
//     }
// }