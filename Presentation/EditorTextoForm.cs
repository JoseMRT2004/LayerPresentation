using System.Windows.Forms;
using AppMultimedia.Data;
using AppMultimedia.Services;

namespace AppMultimedia.Presentation;

public class EditorTextoForm : Form
{
    private TextBox txtEditor = null!;
    private MenuStrip menuStrip = null!;
    private string? rutaActual;
    private readonly EditorTextoService _service;

    public EditorTextoForm()
    {
        _service = new EditorTextoService(new ArchivoService());
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Editor de Texto";
        Size = new Size(700, 500);
        StartPosition = FormStartPosition.CenterScreen;

        menuStrip = new MenuStrip();

        var menuArchivo = new ToolStripMenuItem("Archivo");
        var itemNuevo = new ToolStripMenuItem("Nuevo", null, (s, e) => Nuevo());
        var itemAbrir = new ToolStripMenuItem("Abrir", null, (s, e) => Abrir());
        var itemGuardar = new ToolStripMenuItem("Guardar", null, (s, e) => Guardar());
        var itemGuardarComo = new ToolStripMenuItem("Guardar como...", null, (s, e) => GuardarComo());
        var itemSalir = new ToolStripMenuItem("Salir", null, (s, e) => Close());

        menuArchivo.DropDownItems.Add(itemNuevo);
        menuArchivo.DropDownItems.Add(itemAbrir);
        menuArchivo.DropDownItems.Add(itemGuardar);
        menuArchivo.DropDownItems.Add(itemGuardarComo);
        menuArchivo.DropDownItems.Add(new ToolStripSeparator());
        menuArchivo.DropDownItems.Add(itemSalir);

        menuStrip.Items.Add(menuArchivo);
        Controls.Add(menuStrip);

        txtEditor = new TextBox
        {
            Multiline = true,
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 12),
            ScrollBars = ScrollBars.Both,
            WordWrap = false
        };
        Controls.Add(txtEditor);
    }

    private void Nuevo()
    {
        txtEditor.Clear();
        rutaActual = null;
    }

    private void Abrir()
    {
        using OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Archivos de texto|*.txt|Todos los archivos|*.*";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtEditor.Text = _service.Abrir(dialog.FileName) ?? string.Empty;
            rutaActual = dialog.FileName;
        }
    }

    private void Guardar()
    {
        if (rutaActual != null)
        {
            _service.Guardar(rutaActual, txtEditor.Text);
            MessageBox.Show("Archivo guardado exitosamente", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            GuardarComo();
        }
    }

    private void GuardarComo()
    {
        using SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "Archivos de texto|*.txt|Todos los archivos|*.*";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _service.Guardar(dialog.FileName, txtEditor.Text);
            rutaActual = dialog.FileName;
            MessageBox.Show("Archivo guardado exitosamente", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
