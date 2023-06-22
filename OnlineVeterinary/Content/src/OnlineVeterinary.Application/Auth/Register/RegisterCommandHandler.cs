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
        private readonly IMediator _mediator;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterCommandHandler(IMapper mapper, IMediator mediator, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            
            IRequest<ErrorOr<User>> command = request.RoleType  switch 
            {
                0 => _mapper.Map<AddDoctorCommand>(request),
                1 => _mapper.Map<AddCareGiverCommand>(request),
                _ => _mapper.Map<AddAdminCommand>(request)
                
            };
            
            
            var user =  await _mediator.Send(command);
            var token =   _jwtGenerator.GenerateToken(user.Value);
            var authResult = _mapper.Map<AuthResult>((user,token));

            return authResult;



            
            
            
        }
    }
}