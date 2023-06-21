using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.Reservations.Commands.AddCustom
{
    public class AddReservationCustomCommandValidator : AbstractValidator<AddReservationCustomCommand>
    {
        public AddReservationCustomCommandValidator()
        {
        }
    }
}