namespace AppMultimedia.Services;

public class EditorTextoService
{
    private readonly Data.ArchivoService _archivoService;

    public EditorTextoService(Data.ArchivoService archivoService)
    {
        _archivoService = archivoService;
    }

    public string Nuevo()
    {
        return string.Empty;
    }

    public string? Abrir(string ruta)
    {
        return _archivoService.Leer(ruta);
    }

    public void Guardar(string ruta, string contenido)
    {
        _archivoService.Guardar(ruta, contenido);
    }
}
