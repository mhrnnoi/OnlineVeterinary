using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetByEmail
{
    public class GetCareGiverByEmailQueryValidator : AbstractValidator<GetCareGiverByEmailQuery>
    {
        public GetCareGiverByEmailQueryValidator()
        {
        }
    }
}