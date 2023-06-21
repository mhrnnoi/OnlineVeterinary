using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.Reservations.Queries.GetById
{
    public class GetReservationByIdQueryValidator : AbstractValidator<GetReservationByIdQuery>
    {
        public GetReservationByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("plz enter Id");
        }
    }
}