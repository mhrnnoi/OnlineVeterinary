using System;
using FluentValidation;

namespace OnlineVeterinary.Application.CareGivers.Commands.Update
{
    public class UpdateCareGiverCommandValidator : AbstractValidator<UpdateCareGiverCommand>
    {
        public UpdateCareGiverCommandValidator()
        {
        }
    }
}
