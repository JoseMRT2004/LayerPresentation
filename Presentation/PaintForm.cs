using System.Drawing;

namespace AppMultimedia.Presentation;

public class PaintForm : Form
{
    private PictureBox lienzo = null!;
    private Panel panelColores = null!;
    private Color colorActual = Color.Black;
    private Color colorFondo = Color.White;
    private bool dibujando;
    private Point ultimoPunto;
    private int tamañoPincel = 3;

    public PaintForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Paint - Editor de Dibujo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;

        panelColores = new Panel
        {
            Dock = DockStyle.Left,
            Width = 180,
            BackColor = Color.LightGray
        };
        Controls.Add(panelColores);

        Label lblColores = new Label
        {
            Text = "Paleta de Colores",
            Location = new Point(10, 10),
            AutoSize = true,
            Font = new Font("Arial", 10, FontStyle.Bold)
        };
        panelColores.Controls.Add(lblColores);

        Label lblPincel = new Label
        {
            Text = "Tamaño de pincel:",
            Location = new Point(10, 130),
            AutoSize = true
        };
        panelColores.Controls.Add(lblPincel);

        TrackBar trackPincel = new TrackBar
        {
            Location = new Point(10, 150),
            Minimum = 1,
            Maximum = 20,
            Value = 3,
            Width = 160
        };
        trackPincel.ValueChanged += (s, e) => tamañoPincel = trackPincel.Value;
        panelColores.Controls.Add(trackPincel);

        Label lblFondo = new Label
        {
            Text = "Color de fondo:",
            Location = new Point(10, 190),
            AutoSize = true
        };
        panelColores.Controls.Add(lblFondo);

        Button btnCambiarFondo = new Button
        {
            Text = "Seleccionar fondo...",
            Location = new Point(10, 210),
            Width = 160
        };
        btnCambiarFondo.Click += (s, e) => CambiarColorFondo();
        panelColores.Controls.Add(btnCambiarFondo);

        string[] colores = { "Black", "White", "Red", "Green", "Blue", "Yellow", "Orange", "Purple", "Pink", "Brown", "Gray", "Cyan", "Magenta", "Lime", "Navy", "Maroon" };
        Color[] colorValues = { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange, Color.Purple, Color.Pink, Color.Brown, Color.Gray, Color.Cyan, Color.Magenta, Color.Lime, Color.Navy, Color.Maroon };

        int row = 0, col = 0;
        for (int i = 0; i < colores.Length; i++)
        {
            Button btnColor = new Button
            {
                BackColor = colorValues[i],
                Location = new Point(10 + (col * 40), 250 + (row * 35)),
                Size = new Size(35, 30),
                Tag = colorValues[i]
            };
            if (colorValues[i] == Color.White)
                btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.Click += (s, e) => { if (s is Button b && b.Tag is Color c) colorActual = c; };
            panelColores.Controls.Add(btnColor);

            col++;
            if (col >= 4)
            {
                col = 0;
                row++;
            }
        }

        lienzo = new PictureBox
        {
            Dock = DockStyle.Fill,
            BackColor = colorFondo,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        lienzo.MouseDown += Lienzo_MouseDown;
        lienzo.MouseMove += Lienzo_MouseMove;
        lienzo.MouseUp += Lienzo_MouseUp;
        Controls.Add(lienzo);

        ActualizarLienzo();
    }

    private void CambiarColorFondo()
    {
        using ColorDialog dialog = new ColorDialog();
        dialog.Color = colorFondo;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            colorFondo = dialog.Color;
            lienzo.BackColor = colorFondo;
            ActualizarLienzo();
        }
    }

    private void ActualizarLienzo()
    {
        Bitmap bmp = new Bitmap(lienzo.Width, lienzo.Height);
        using Graphics g = Graphics.FromImage(bmp);
        g.Clear(colorFondo);
        lienzo.Image = bmp;
    }

    private void Lienzo_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            dibujando = true;
            ultimoPunto = e.Location;
        }
    }

    private void Lienzo_MouseMove(object? sender, MouseEventArgs e)
    {
        if (dibujando && lienzo.Image != null)
        {
            using Graphics g = Graphics.FromImage(lienzo.Image);
            using Pen pen = new Pen(colorActual, tamañoPincel);
            g.DrawLine(pen, ultimoPunto, e.Location);
            lienzo.Invalidate();
            ultimoPunto = e.Location;
        }
    }

    private void Lienzo_MouseUp(object? sender, MouseEventArgs e)
    {
        dibujando = false;
    }
}
