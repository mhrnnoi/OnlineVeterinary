using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets
{
    public class GetMyPetsQueryValidator : AbstractValidator<GetMyPetsQuery>
    {
        public GetMyPetsQueryValidator()
        {
        }
    }
}
