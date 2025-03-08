using Application.Abstractions;

namespace Application.Commands.Donuts
{
    /// <summary>
    /// Comando para crear donitas del Krispy Kreme
    /// </summary>
    public class CreateDonutCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
