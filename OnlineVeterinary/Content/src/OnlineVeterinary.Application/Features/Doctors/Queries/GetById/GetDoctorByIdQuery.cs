using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;

namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetById
{
    public record GetDoctorByIdQuery(Guid Id) : IRequest<ErrorOr<DoctorDTO>>;
    
}