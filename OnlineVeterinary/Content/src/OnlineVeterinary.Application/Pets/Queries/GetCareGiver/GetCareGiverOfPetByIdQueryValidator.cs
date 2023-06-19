using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Pets.Queries.GetCareGiver
{
    public class GetCareGiverOfPetByIdQueryValidator : AbstractValidator<GetCareGiverOfPetByIdQuery>
    {
        public GetCareGiverOfPetByIdQueryValidator()
        {
        }
    }
}
