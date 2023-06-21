using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Admin.Commands;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.CareGivers.Commands.Add;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Doctors.Commands.Add;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            
            
            if (request.RoleType == 0)
            {
                var command =  _mapper.Map<AddDoctorCommand>(request);
                await _mediator.Send(command);
                

            }
            else if (request.RoleType == 1)
            {
                var command =  _mapper.Map<AddCareGiverCommand>(request);
                await _mediator.Send(command);
           

            }
            else if (request.RoleType == 2)
            {
                var command =  _mapper.Map<AddAdminCommand>(request);
                await _mediator.Send(command);
          

            }
            else 
            {
                return Error.Failure();
            }
            
            var user =  _mapper.Map<User>(request);
            var token =   _jwtGenerator.GenerateToken(user);
            var authResult = _mapper.Map<AuthResult>((user,token));

            return authResult;



            
            
            
        }
    }
}