using System.Text.RegularExpressions;
using FluentValidation;

namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetById
{
    public class GetDoctorByIdQueryValidator : AbstractValidator<GetDoctorByIdQuery>
    {
        private static readonly string _reg = @"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";
        private static readonly Regex _regex = new Regex(_reg);

        public GetDoctorByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("plz enter Id");
            RuleFor(z=> z.Id.ToString().Length).GreaterThan(35);
            RuleFor(z=> _regex.IsMatch(z.Id.ToString())).Equal(true);
        }
    }
}