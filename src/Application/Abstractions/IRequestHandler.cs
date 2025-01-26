using Application.Models;

namespace Application.Abstractions
{
    /// <summary>
    /// Representa un manejador de solicitud de cliente
    /// </summary>
    /// <typeparam name="TRequest">Tipo de solicitud</typeparam>
    public interface IRequestHandler<TRequest> :
        MediatR.IRequestHandler<TRequest, Result> where TRequest : IRequest
    {
    }

    /// <summary>
    /// Representa un manejador de solicitud de cliente que espera un valor
    /// </summary>
    /// <typeparam name="TRequest">Tipo de solicitud</typeparam>
    /// <typeparam name="TValue">Tipo de valor</typeparam>
    public interface IRequestHandler<TRequest,TValue> : 
        MediatR.IRequestHandler<TRequest,Result<TValue>> where TRequest : IRequest<TValue>
    {
    }
}
