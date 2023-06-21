using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.CareGivers.Commands.DeleteById;
using OnlineVeterinary.Application.CareGivers.Queries.GetByEmail;
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
            
            Guid id;
            ErrorOr<string> deleteResult;

            
            if (request.RoleType == 0)
            {
                var command = _mapper.Map<GetDoctorByEmailQuery>(request.Email);
                var result = await _mediator.Send(command);
                id = result.Value.Id;
                if (result.IsError)
                {
                    return Error.Validation(Error.Validation().Code, "Password or email is incorrect");

                }
                if (result.Value.Password == request.Password)
                {
                    var deleteCommand = new DeleteDoctorByIdCommand(id);
                    deleteResult = await _mediator.Send(deleteCommand);
                }
                else
                {
                    return Error.Validation(Error.Validation().Code, "Password or email is incorrect");
                }


            }
            else if (request.RoleType == 1)
            {
                var command = _mapper.Map<GetCareGiverByEmailQuery>(request.Email);
                var result = await _mediator.Send(command);
                id = result.Value.Id;
                if (result.IsError)
                {
                    return Error.Validation(Error.Validation().Code, "Password or email is incorrect");

                }
                if (result.Value.Password == request.Password)
                {
                    var deleteCommand = new DeleteCareGiverByIdCommand(id);
                    deleteResult = await _mediator.Send(deleteCommand);
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

            return deleteResult;
        }
    }
}