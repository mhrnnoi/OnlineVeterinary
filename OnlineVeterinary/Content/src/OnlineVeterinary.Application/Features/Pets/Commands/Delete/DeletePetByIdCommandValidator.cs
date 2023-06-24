using System.Text.RegularExpressions;
using FluentValidation;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Delete
{
    public class DeletePetByIdCommandValidator : AbstractValidator<DeletePetByIdCommand>
    {
         private static readonly string _reg = @"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";
        private static readonly Regex _regex = new Regex(_reg);
        public DeletePetByIdCommandValidator()
        {
            RuleFor(z=> z.Id).NotEmpty();
            RuleFor(z=> z.Id.ToString().Length).GreaterThan(35);
            RuleFor(z=> _regex.IsMatch(z.Id.ToString())).Equal(true).WithMessage("plz enter valid id with guid format");
        }
    }
}
