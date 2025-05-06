using Application.RP;
using MediatR;

namespace Application.Commands.Donuts
{
    /// <summary>
    /// Comando para crear una donita del Krispy Kreme. 
    /// Devuelve el ID de la donita creada.
    /// </summary>
    public class CreateDonutCommand : IRequest<Result<int>>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
