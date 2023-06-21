using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.CareGivers.Queries.GetByEmail;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Doctors.Queries.GetByEmail;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthResult>>
    {
        private readonly ICareGiverRepository _careGiverRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public LoginCommandHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _careGiverRepository = careGiverRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<ErrorOr<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            
            
           if (request.RoleType == 0)
            {
                var command =  _mapper.Map<GetDoctorByEmailQuery>(request);
                var result =  await _mediator.Send(command);
                

            }
            else if (request.RoleType == 1)
            {
                var command =  _mapper.Map<GetCareGiverByEmailQuery>(request);
                var result =  await _mediator.Send(command);
           

            }
            // else if (request.RoleType == 2)
            // {
            //     var command =  _mapper.Map<AddAdminCommand>(request);
            //     var result =  await _mediator.Send(command);
          

            // }
            else 
            {
                return Error.Failure();
            }
            
            
            // var token =   _jwtGenerator.GenerateToken(request);
            // var authResult = _mapper.Map<AuthResult>((request,token));

            return new ErrorOr<AuthResult>();


            
        }
    }
}