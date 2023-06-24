using FluentValidation;


namespace OnlineVeterinary.Application.Features.Doctors.Queries.GetAll
{
    public class GetAllDoctorsQueryValidator : AbstractValidator<GetAllDoctorsQuery>
    {
        public GetAllDoctorsQueryValidator()
        {
        }
    }
}