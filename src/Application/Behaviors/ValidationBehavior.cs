using Application.RP;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : IResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                Error[] errors = await ValidateAsync(request, cancellationToken);

                if (errors.Length > 0)
                {
                    var result = (TResponse?)Activator.CreateInstance(typeof(TResponse), true);
                    _ = result ?? throw new Exception("No se pudo crear una instancia del resultado");
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Errors = errors;
                    return result;
                }
            }

            return await next(cancellationToken);
        }

        private async Task<Error[]> ValidateAsync(TRequest request, CancellationToken cancellationToken)
        {
            var valContext = new ValidationContext<TRequest>(request);
            var valTasks = _validators.Select(v => v.ValidateAsync(valContext, cancellationToken));
            ValidationResult[] valResults = await Task.WhenAll(valTasks);

            var errors = valResults.Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .Select(x => new Error("400", x.ErrorMessage))
                .ToArray();

            return errors;
        }
    }
}
