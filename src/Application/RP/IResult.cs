using System.Net;

namespace Application.RP
{
    public interface IResult
    {
        HttpStatusCode StatusCode { get; set; }
        IEnumerable<Error>? Errors { get; set; }
    }
}
