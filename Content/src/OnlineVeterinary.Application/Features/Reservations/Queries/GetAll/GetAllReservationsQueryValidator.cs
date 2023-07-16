using FluentValidation;

namespace OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll
{
    public class GetAllReservationsQueryValidator : AbstractValidator<GetAllReservationsQuery>
    {
        public GetAllReservationsQueryValidator()
        {
        }
    }
}