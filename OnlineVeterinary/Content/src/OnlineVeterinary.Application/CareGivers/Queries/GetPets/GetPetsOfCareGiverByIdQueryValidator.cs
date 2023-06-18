using System;
using FluentValidation;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetPets
{
    public class GetPetsOfCareGiverByIdQueryValidator : AbstractValidator<GetPetsOfCareGiverByIdQuery>
    {
        public GetPetsOfCareGiverByIdQueryValidator()
        {
        }
    }
}
