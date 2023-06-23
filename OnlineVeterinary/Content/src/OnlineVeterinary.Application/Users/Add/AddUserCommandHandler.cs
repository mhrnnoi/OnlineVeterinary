// using System;
// using ErrorOr;
// using MapsterMapper;
// using MediatR;
// using OnlineVeterinary.Application.Common.Interfaces;
// using OnlineVeterinary.Application.Common.Interfaces.Persistence;
// using OnlineVeterinary.Application.DTOs;
// using OnlineVeterinary.Domain.Doctor.Entities;
// using OnlineVeterinary.Domain.Users.Entities;

// namespace OnlineVeterinary.Application.Users.Commands.Add
// {
//     public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ErrorOr<User>>
//     {
//         private readonly IUserRepository _userrepository;
//         private readonly IMapper _mapper;
//         private readonly IUnitOfWork _unitOfWork;

//         public AddUserCommandHandler(IUserRepository userrepository, IMapper mapper, IUnitOfWork unitOfWork)
//         {
//             _userrepository = userrepository;
//             _mapper = mapper;
//             _unitOfWork = unitOfWork;
//         }
//         public async Task<ErrorOr<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
//         {
//             var user = _mapper.Map<User>(request);
//             await _userrepository.AddAsync(user);

//             await _unitOfWork.SaveChangesAsync();

//             return user;
//         }
//     }
// }
