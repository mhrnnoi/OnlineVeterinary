using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineVeterinary.Application.Admins.Queries;
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
        private readonly IMediator _mediator;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginCommandHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<ErrorOr<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {


            IRequest<ErrorOr<User>> command = request.RoleType switch
            {
                0 => _mapper.Map<GetDoctorByEmailQuery>(request),
                1 => _mapper.Map<GetCareGiverByEmailQuery>(request),
                _ => _mapper.Map<GetAdminByEmailQuery>(request)

            };

            var user = await _mediator.Send(command);

            if (user.IsError || user.Value.Password != request.Password)
            {
                return Error.Validation(Error.Validation().Code, "Password or email is incorrect");

            }

            var token = _jwtGenerator.GenerateToken(user.Value);
            var authResult = _mapper.Map<AuthResult>((user.Value, token));


            return authResult;



        }
    }
}