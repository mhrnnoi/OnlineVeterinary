using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll
{
    public class GetAllReservationsQueryValidator : AbstractValidator<GetAllReservationsQuery>
    {
        public GetAllReservationsQueryValidator()
        {
        }
    }
}