using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.CareGivers.Commands.Add
{
    public class AddCareGiverCommandValidator : AbstractValidator<AddCareGiverCommand>
    {
        public AddCareGiverCommandValidator()
        {
        }
    }
}