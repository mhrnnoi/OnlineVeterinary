using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using OnlineVeterinary.Application.DTOs.CareGiverDTO;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetAll
{
    public class GetAllCareGiversQueryValidator : AbstractValidator<GetAllCareGiversQuery>
    {
        public GetAllCareGiversQueryValidator()
        {
        }
    }
}