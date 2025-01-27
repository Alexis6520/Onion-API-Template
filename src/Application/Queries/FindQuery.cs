using Application.Abstractions;

namespace Application.Queries
{
    /// <summary>
    /// Consulta genérica por alguna clave o llave
    /// </summary>
    /// <typeparam name="TKey">Tipo de clave</typeparam>
    /// <typeparam name="TValue">Tipo de valor</typeparam>
    /// <param name="key">Clave</param>
    public class FindQuery<TKey, TValue>(TKey key) : IRequest<TValue>
    {
        public TKey Key { get; set; } = key;
    }
}
