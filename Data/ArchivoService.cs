using System.IO;

namespace AppMultimedia.Data;

public class ArchivoService
{
    public string? Leer(string ruta)
    {
        return File.ReadAllText(ruta);
    }

    public void Guardar(string ruta, string contenido)
    {
        File.WriteAllText(ruta, contenido);
    }
}
