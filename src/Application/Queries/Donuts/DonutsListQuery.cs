using Application.Abstractions;
using Application.Models.Donuts;

namespace Application.Queries.Donuts
{
    public class DonutsListQuery : IRequest<List<DonutListItem>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
