# Aplicación Multimedia

Proyecto Windows Forms con arquitectura de 3 capas.

## Estructura de Capas

### 1. Capa de Presentación (`/Presentation`)
Maneja la **interfaz gráfica** (UI). Aquí están los formularios Windows Forms:
- `MenuPrincipal.cs`, `CalculadoraForm.cs`, `EditorTextoForm.cs`, `PaintForm.cs`
- Solo se encarga de mostrar información y capturar interacciones del usuario
- **No contiene lógica de negocio**

### 2. Capa de Servicios / Lógica de Negocio (`/Services`)
Contiene la **lógica de la aplicación**:
- `CalculadoraService.cs` - operaciones matemáticas
- `EditorTextoService.cs` - procesamiento de texto
- **Isla la lógica del UI**: si cambias la interfaz, la lógica permanece igual

### 3. Capa de Datos (`/Data`)
Maneja el **acceso a datos**:
- `ArchivoService.cs` - lectura/escritura de archivos
- Abstrae cómo se almacenan los datos

---

## Flujo de Datos

```
Usuario → Presentación → Servicios → Datos → Archivo/BD
```

Esta separación facilita el mantenimiento: puedes cambiar la UI sin afectar la lógica, o cambiar el almacenamiento sin tocar el resto del código.

## Requisitos

- .NET 8.0 o superior
- Windows Forms

## Ejecutar

```bash
dotnet run
```
