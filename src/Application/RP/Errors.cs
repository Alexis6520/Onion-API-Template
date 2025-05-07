namespace Application.RP
{
    /// <summary>
    /// Catálogo de errores de dominio.
    /// </summary>
    public static class Errors
    {
        // Genéricos
        public static Error NOT_FOUND => new("404", "Recurso no encontrado");
        public static Error UNHANDLED_ERROR => new("500", "Hubo un problema al procesar la solicitud");

        // Específicos
        public static Error DONUT_NAME_NOT_AVAILABLE => new("01", "Ya existe una donita con ese nombre, mi chavo :(");
    }
}
