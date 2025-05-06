using Application.RP;
using MediatR;

namespace Application.Queries
{
    /// <summary>
    /// Query genérica para buscar un recurso por alguna clave.
    /// </summary>
    /// <typeparam name="TKey">Tipo de clave</typeparam>
    /// <typeparam name="TValue">Tipo de valor</typeparam>
    public class FindQuery<TKey, TValue>(TKey key) : IRequest<Result<TValue>>
    {
        public TKey Key { get; set; } = key;
    }
}
