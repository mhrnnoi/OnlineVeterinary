using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Pets.Queries.GetAll
{
    public class GetAllPetsQueryValidator : AbstractValidator<GetAllPetsQuery>
    {
        public GetAllPetsQueryValidator()
        {
        }
    }
}
