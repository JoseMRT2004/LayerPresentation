using System.Drawing;
using System.Windows.Forms;
using AppMultimedia.Services;

namespace AppMultimedia.Presentation;

public class MenuPrincipal : Form
{
    public MenuPrincipal()
    {
        Text = "Aplicación Multimedia";
        Size = new Size(400, 300);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Label lblTitulo = new Label
        {
            Text = "Seleccione una opción:",
            Location = new Point(100, 30),
            AutoSize = true,
            Font = new Font("Arial", 14, FontStyle.Bold)
        };
        Controls.Add(lblTitulo);

        Button btnEditorTexto = new Button
        {
            Text = "1. Editor de Texto",
            Location = new Point(75, 80),
            Size = new Size(250, 40),
            Font = new Font("Arial", 12)
        };
        btnEditorTexto.Click += (s, e) => new EditorTextoForm().ShowDialog();
        Controls.Add(btnEditorTexto);

        Button btnPaint = new Button
        {
            Text = "2. Paint",
            Location = new Point(75, 140),
            Size = new Size(250, 40),
            Font = new Font("Arial", 12)
        };
        btnPaint.Click += (s, e) => new PaintForm().ShowDialog();
        Controls.Add(btnPaint);

        Button btnCalculadora = new Button
        {
            Text = "3. Calculadora",
            Location = new Point(75, 200),
            Size = new Size(250, 40),
            Font = new Font("Arial", 12)
        };
        btnCalculadora.Click += (s, e) => new CalculadoraForm().ShowDialog();
        Controls.Add(btnCalculadora);
    }
}
