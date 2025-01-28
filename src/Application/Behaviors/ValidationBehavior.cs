using Application.Models;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.Behaviors
{
    /// <summary>
    /// Pipeline de validación de solicitudes
    /// </summary>
    /// <typeparam name="TRequest">Tipo de solicitud</typeparam>
    /// <typeparam name="TResponse">Tipo de respuesta</typeparam>
    /// <param name="validators">Validadores de solicitud</param>
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
        IPipelineBehavior<TRequest, TResponse> where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);
                var results = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(validationContext)));
                var fails = results.Where(x => !x.IsValid).SelectMany(x => x.Errors);

                if (fails.Any())
                {
                    var result = Activator.CreateInstance<TResponse>();
                    result.Succeeded = false;
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Errors = fails.Select(x => x.ErrorMessage).ToList();
                    return result;
                }
            }

            return await next();
        }
    }
}
