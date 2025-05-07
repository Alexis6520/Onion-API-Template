using System.Net;
using System.Text.Json.Serialization;

namespace Application.RP
{
    /// <summary>
    /// Representa el resultado de una operación que puede ser exitosa o fallida
    /// dentro del proceso de negocio.
    /// </summary>
    /// <typeparam name="T">Tipo de valor devuelto</typeparam>
    public class Result<T> : IResult
    {
        private Result() { }

        [JsonIgnore]
        public bool Succeeded => Errors is null || !Errors.Any();

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Value { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<Error>? Errors { get; set; }

        /// <summary>
        /// Crea un resultado exitoso con el valor especificado y el código de estado HTTP.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Result<T> Success(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (statusCode >= HttpStatusCode.BadRequest)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(statusCode),
                    statusCode,
                    "El código de estado HTTP debe ser menor que 400 para un resultado exitoso.");
            }

            return new Result<T>
            {
                StatusCode = statusCode,
                Value = value
            };
        }

        /// <summary>
        /// Crea un resultado exitoso sin valor y con el código de estado HTTP especificado.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static Result<Unity> Success(HttpStatusCode statusCode = HttpStatusCode.NoContent)
        {
            return Result<Unity>.Success(new Unity(), statusCode);
        }

        /// <summary>
        /// Crea un resultado fallido con el código de estado HTTP y los errores especificados.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Result<T> Failure(HttpStatusCode statusCode, params Error[] errors)
        {
            if (statusCode < HttpStatusCode.BadRequest)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(statusCode),
                    statusCode,
                    "El código de estado HTTP debe ser mayor o igual a 400 para un resultado fallido.");
            }
            return new Result<T>
            {
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }
}
