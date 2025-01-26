using Application.Models;

namespace Application.Abstractions
{
    /// <summary>
    /// Representa la solicitud de un cliente
    /// </summary>
    public interface IRequest : MediatR.IRequest<Result>
    {
    }

    /// <summary>
    /// Representa la solicitud de un cliente que espera un valor
    /// </summary>
    /// <typeparam name="TValue">Tipo de valor</typeparam>
    public interface IRequest<TValue> : MediatR.IRequest<Result<TValue>>
    {
    }
}
