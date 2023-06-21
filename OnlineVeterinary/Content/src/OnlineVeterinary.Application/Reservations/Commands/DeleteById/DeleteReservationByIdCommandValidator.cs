using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.Reservations.Commands.DeleteById
{
    public class DeleteReservationByIdCommandValidator : AbstractValidator<DeleteReservationByIdCommand>
    {
        public DeleteReservationByIdCommandValidator()
        {
        }
    }
}