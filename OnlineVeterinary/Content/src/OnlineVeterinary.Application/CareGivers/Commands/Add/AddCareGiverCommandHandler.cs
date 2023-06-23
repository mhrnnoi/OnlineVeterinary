// using System;
// using ErrorOr;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;
// using OnlineVeterinary.Domain.CareGivers.Entities;

// namespace OnlineVeterinary.Application.CareGivers.Commands.Add
// {
//     public class AddCareGiverCommandHandler : IRequestHandler<AddCareGiverCommand, ErrorOr<User>>
//     {
//         private readonly ICareGiverRepository _careGiverRepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public AddCareGiverCommandHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _careGiverRepository = careGiverRepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<ErrorOr<User>> Handle(AddCareGiverCommand request, CancellationToken cancellationToken)
//         {
//             var careGiver = _mapper.Map<CareGiver>(request);
//             await _careGiverRepository.AddAsync(careGiver);
//             var User = _mapper.Map<User>(request);
//             await _unitOfWork.SaveChangesAsync();

//             return User;
//         }
//     }
// }
