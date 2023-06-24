using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;

namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetAll
{
    public record GetAllDoctorsQuery() : IRequest<ErrorOr<List<DoctorDTO>>>;

}
