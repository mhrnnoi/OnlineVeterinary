using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Pets.Queries.GetPet
{
    public class GetPetByIdQueryValidator : AbstractValidator<GetPetByIdQuery>
    {
        public GetPetByIdQueryValidator()
        {
        }
    }
}
