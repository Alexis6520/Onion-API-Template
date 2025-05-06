namespace Application.RP
{
    /// <summary>
    /// Catálogo de errores de dominio.
    /// </summary>
    public static class Errors
    {
        public static Error DONUT_NAME_NOT_AVAILABLE => new("01", "Ya existe una donita con ese nombre, mi chavo :(");
    }
}
