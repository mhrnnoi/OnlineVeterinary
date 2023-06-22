using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Admins.Commands;
using OnlineVeterinary.Application.Admins.Queries;
using OnlineVeterinary.Application.Auth.Login;
using OnlineVeterinary.Application.CareGivers.Commands.DeleteById;
using OnlineVeterinary.Application.CareGivers.Queries.GetByEmail;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Doctors.Commands.DeleteById;
using OnlineVeterinary.Application.Doctors.Queries.GetByEmail;

namespace OnlineVeterinary.Application.Auth.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, ErrorOr <string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IJwtGenerator _jwtGenerator;

        public DeleteCommandHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<ErrorOr<string>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var loginCommand = _mapper.Map<LoginCommand>(request);
            var loginResult = await _mediator.Send(loginCommand);
            if (loginResult.IsError)
            {
                return Error.Failure();
            }
            var user = loginResult.Value;
           
            IRequest<ErrorOr<string>> deleteCommand = request.RoleType switch
            {
                0 =>  new DeleteDoctorByIdCommand(user.Id),
                1 =>  new DeleteCareGiverByIdCommand(user.Id),
                _ =>  new DeleteAdminByIdCommand(user.Id),
            };
            var deleteResult = await _mediator.Send(deleteCommand);

            return deleteResult;
            
        }
    }
}