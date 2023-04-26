﻿using ChatApplication.Domain.Shared;
using FluentValidation;
using MediatR;

namespace ChatApplication.Application.Behaviors
{
    public class ValidationPipeLineBeahvior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result 
    {

        private IEnumerable<IValidator<TRequest>> _validators; 

        public ValidationPipeLineBeahvior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);

            Error[] errors = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validatonResult => validatonResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .Distinct()
                .Select(failure => new Error(failure.PropertyName,failure.ErrorMessage))
                .ToArray();

            if (errors.Length > 0)
            {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors)
            where TResult : Result
        {
            if (typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }

            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))!
                .Invoke(null, new object[] { errors })!;
            
            return (TResult)validationResult;
        }
    }
}
