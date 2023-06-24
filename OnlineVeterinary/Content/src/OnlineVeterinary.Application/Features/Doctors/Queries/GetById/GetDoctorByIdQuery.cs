using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;

namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetById
{
    public record GetDoctorByIdQuery(Guid Id) : IRequest<ErrorOr<DoctorDTO>>;
    
}