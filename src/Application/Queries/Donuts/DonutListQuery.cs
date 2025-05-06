using Application.Models.Donuts;
using MediatR;

namespace Application.Queries.Donuts
{
    /// <summary>
    /// Consulta de lista de donas
    /// </summary>
    /// <param name="pageSize"></param>
    public class DonutListQuery(int pageSize) : IRequest<List<DonutItem>>
    {
        public int PageSize { get; set; } = pageSize;
    }
}
