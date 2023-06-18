using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>  where  TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public ValidationBehavior(IValidator<TRequest> validator, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request,cancellationToken);
            if (result.IsValid)
            {
                return await next();
            }
            await _unitOfWork.DisposeAsync();
            var errors =  string.Join(",\n", result.Errors.ToList());
            throw new Exception(errors);
        }
    }
}