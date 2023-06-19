using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.Doctors.Commands.DeleteById
{
    public class DeleteDoctorByIdCommandValidator : AbstractValidator<DeleteDoctorByIdCommand>
    {
        public DeleteDoctorByIdCommandValidator()
        {
        }
    }
}