using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;

namespace OnlineVeterinary.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>  where  TRequest : IRequest<TResponse> where TResponse : IErrorOr
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
            //this dynamic i use , is for simplcity i know dynamic is dangerous :)
            return (dynamic)Error.Validation(result.Errors.First().PropertyName,result.Errors.First().ErrorMessage );

        }
    }
}