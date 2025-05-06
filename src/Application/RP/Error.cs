namespace Application.RP
{
    /// <summary>
    /// Representa un error de dominio que forma parte del proceso de negocio.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="description"></param>
    public class Error(string code, string description)
    {
        public string Code { get; set; } = code;
        public string Description { get; set; } = description;
    }
}
