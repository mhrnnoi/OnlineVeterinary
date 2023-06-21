using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Auth.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<CareGiverDTO>>
    {
         private readonly ICareGiverRepository _careGiverRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(ICareGiverRepository careGiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _careGiverRepository = careGiverRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<ErrorOr<CareGiverDTO>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}