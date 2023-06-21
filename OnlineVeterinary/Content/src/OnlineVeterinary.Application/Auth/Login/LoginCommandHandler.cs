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
using OnlineVeterinary.Application.Common;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Doctors.Queries.GetByEmail;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthResult>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginCommandHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<ErrorOr<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            AuthResult authResult;
            Jwt token;
            User user;
            if (request.RoleType == 0)
            {
                var command = _mapper.Map<GetDoctorByEmailQuery>(request);
                var result = await _mediator.Send(command);

                if (result.IsError)
                {
                    return Error.Validation(Error.Validation().Code, "Password or email is incorrect");

                }
                if (result.Value.Password == request.Password)
                {
                    user = _mapper.Map<User>(result);
                    token = _jwtGenerator.GenerateToken(user);
                    authResult = _mapper.Map<AuthResult>((user, token));
                }
                else
                {
                    return Error.Validation(Error.Validation().Code, "Password or email is incorrect");
                }


            }
            else if (request.RoleType == 1)
            {
                var command = _mapper.Map<GetCareGiverByEmailQuery>(request);
                var result = await _mediator.Send(command);
                if (result.IsError)
                {
                    return Error.Validation(Error.Validation().Code, "Password or email is incorrect");

                }
                if (result.Value.Password == request.Password)
                {
                    user = _mapper.Map<User>(result);
                    token = _jwtGenerator.GenerateToken(user);
                    authResult = _mapper.Map<AuthResult>((user, token));
                }
                else
                {
                    return Error.Validation(Error.Validation().Code, "Password or email is incorrect");
                }

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

            return authResult;



        }
    }
}