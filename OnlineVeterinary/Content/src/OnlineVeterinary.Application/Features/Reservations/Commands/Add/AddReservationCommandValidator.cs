using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;


namespace OnlineVeterinary.Application.Features.Reservations.Commands.Add
{
    public class AddReservationCommandValidator : AbstractValidator<AddReservationCommand>
    {
        private static readonly string _reg = @"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";
        private static readonly Regex _regex = new Regex(_reg);

       
        public AddReservationCommandValidator()
        {
            RuleFor(z=> z.PetId).NotEmpty();
            RuleFor(z=> z.PetId.ToString().Length).GreaterThan(35);
            RuleFor(z=> _regex.IsMatch(z.PetId.ToString())).Equal(true);
            
            
        }
    }
}