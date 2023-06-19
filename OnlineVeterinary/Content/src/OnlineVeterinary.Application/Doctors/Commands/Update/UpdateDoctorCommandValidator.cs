using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Doctors.Commands.Update
{
    public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator()
        {
        }
    }
}
