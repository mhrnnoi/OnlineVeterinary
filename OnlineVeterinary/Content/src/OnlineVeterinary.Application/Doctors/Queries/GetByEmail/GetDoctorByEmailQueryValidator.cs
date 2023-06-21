using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.Doctors.Queries.GetByEmail
{
    public class GetDoctorByEmailQueryValidator : AbstractValidator<GetDoctorByEmailQuery>
    {
        public GetDoctorByEmailQueryValidator()
        {
        }
    }
}