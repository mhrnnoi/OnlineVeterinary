using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.Features.Auth.Commands.Delete
{
    public class DeleteMyAccountCommandValidator : AbstractValidator<DeleteMyAccountCommand>
    {
        public DeleteMyAccountCommandValidator()
        {
        }
    }
}