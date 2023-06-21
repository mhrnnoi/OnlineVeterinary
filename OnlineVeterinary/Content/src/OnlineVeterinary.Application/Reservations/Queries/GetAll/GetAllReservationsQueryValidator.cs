using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using OnlineVeterinary.Application.ReservedTimes.Queries.GetAll;

namespace OnlineVeterinary.Application.Reservations.Queries.GetAll
{
    public class GetAllReservationsQueryValidator : AbstractValidator<GetAllReservationsQuery>
    {
        public GetAllReservationsQueryValidator()
        {
        }
    }
}