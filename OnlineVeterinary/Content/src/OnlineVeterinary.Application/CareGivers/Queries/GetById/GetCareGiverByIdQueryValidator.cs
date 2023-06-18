using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetById
{
    public class GetCareGiverByIdQueryValidator : AbstractValidator<GetCareGiverByIdQuery>
    {
        public GetCareGiverByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("plz enter Id");
        }
    }
}