using FluentValidation;

namespace OnlineVeterinary.Application.Features.Auth.Commands.Delete
{
    public class DeleteMyAccountCommandValidator 
    : AbstractValidator<DeleteMyAccountCommand>
    {
        public DeleteMyAccountCommandValidator() 
        {
            
        }
    }
}