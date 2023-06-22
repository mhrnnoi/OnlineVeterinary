using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Doctors.Commands.DeleteById
{
    public class DeleteDoctorByIdCommandHandler : IRequestHandler<DeleteDoctorByIdCommand, ErrorOr<string>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDoctorByIdCommandHandler(IDoctorRepository doctorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            await _doctorRepository.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            return "Deleted Succesfuly";
        }
    }
}