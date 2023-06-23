using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Doctors.Queries.GetAll
{
    public record GetAllDoctorsQuery() : IRequest<ErrorOr<List<DoctorDTO>>>;

}