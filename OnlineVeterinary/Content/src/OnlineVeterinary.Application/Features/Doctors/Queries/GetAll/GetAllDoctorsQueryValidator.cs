using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;


namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetAll
{
    public class GetAllDoctorsQueryValidator : AbstractValidator<GetAllDoctorsQuery>
    {
        public GetAllDoctorsQueryValidator()
        {
        }
    }
}