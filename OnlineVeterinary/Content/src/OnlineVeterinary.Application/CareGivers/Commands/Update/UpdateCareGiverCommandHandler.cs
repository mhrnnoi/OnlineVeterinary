// using System;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;
// using OnlineVeterinary.Domain.CareGivers.Entities;

// namespace OnlineVeterinary.Application.CareGivers.Commands.Update
// {
//     public class UpdateCareGiverCommandHandler : IRequestHandler<UpdateCareGiverCommand, CareGiverDTO>
//     {
//         private readonly ICareGiverRepository _careGiverRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public UpdateCareGiverCommandHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _careGiverRepository = careGiverRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<CareGiverDTO> Handle(UpdateCareGiverCommand request, CancellationToken cancellationToken)
//         {
//             var careGiver = _mapper.Map<CareGiver>(request);
//             await _careGiverRepository.UpdateAsync(careGiver);
//             await _unitOfWork.SaveChangesAsync();

//             return _mapper.Map<CareGiverDTO>(request);
//         }
//     }
// }
