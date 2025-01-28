using Application.Abstractions;

namespace Application.Commands
{
    /// <summary>
    /// Comando genérico de eliminación de recursos
    /// </summary>
    /// <typeparam name="TKey">Tipo de Clave/Id del recurso</typeparam>
    /// <typeparam name="TResource">Tipo de recurso</typeparam>
    /// <param name="key">Clave/Id del recurso</param>
    public class DeleteCommand<TKey,TResource>(TKey key) : IRequest
    {
        public TKey Key { get; set; } = key;
    }
}
