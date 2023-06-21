using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Reservations.Commands.Update
{
    public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationCommandValidator()
        {
        }
    }
}
