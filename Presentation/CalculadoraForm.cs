using System.Drawing;
using System.Windows.Forms;
using AppMultimedia.Services;

namespace AppMultimedia.Presentation;

public class CalculadoraForm : Form
{
    private TextBox txtDisplay = null!;
    private string operador = "";
    private double primerNumero;
    private bool nuevoNumero = true;
    private readonly CalculadoraService _service;

    public CalculadoraForm()
    {
        _service = new CalculadoraService();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Calculadora";
        Size = new Size(320, 400);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        txtDisplay = new TextBox
        {
            Location = new Point(10, 10),
            Size = new Size(280, 40),
            Font = new Font("Arial", 18),
            TextAlign = HorizontalAlignment.Right,
            ReadOnly = true,
            BackColor = Color.White
        };
        Controls.Add(txtDisplay);

        string[] botones = {
            "C", "±", "%", "/",
            "7", "8", "9", "*",
            "4", "5", "6", "-",
            "1", "2", "3", "+",
            "0", ".", "="
        };

        int x = 10, y = 60;
        int indice = 0;

        for (int fila = 0; fila < 5; fila++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (indice >= botones.Length) break;

                Button btn = new Button
                {
                    Text = botones[indice],
                    Location = new Point(x + (col * 70), y + (fila * 50)),
                    Size = new Size(65, 45),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    BackColor = Color.LightGray
                };

                if (botones[indice] == "C")
                    btn.BackColor = Color.Salmon;
                else if (new[] { "/", "*", "-", "+", "=" }.Contains(botones[indice]))
                    btn.BackColor = Color.LightBlue;
                else if (botones[indice] == "%")
                    btn.BackColor = Color.Khaki;

                btn.Click += Boton_Click;
                Controls.Add(btn);
                indice++;
            }
        }
    }

    private void Boton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        string texto = btn.Text;

        if (texto == "C")
        {
            txtDisplay.Text = "";
            primerNumero = 0;
            operador = "";
            nuevoNumero = true;
        }
        else if (texto == "±")
        {
            txtDisplay.Text = _service.CambiarSigno(txtDisplay.Text);
        }
        else if (texto == "%")
        {
            if (txtDisplay.Text != "")
            {
                txtDisplay.Text = _service.Porcentaje(Convert.ToDouble(txtDisplay.Text)).ToString();
            }
        }
        else if (new[] { "/", "*", "-", "+" }.Contains(texto))
        {
            if (txtDisplay.Text != "")
            {
                primerNumero = Convert.ToDouble(txtDisplay.Text);
                operador = texto;
                nuevoNumero = true;
            }
        }
        else if (texto == "=")
        {
            if (txtDisplay.Text != "" && operador != "")
            {
                try
                {
                    double segundoNumero = Convert.ToDouble(txtDisplay.Text);
                    double resultado = _service.Operar(primerNumero, segundoNumero, operador);
                    txtDisplay.Text = resultado.ToString();
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show("No se puede dividir por cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                operador = "";
                nuevoNumero = true;
            }
        }
        else if (texto == ".")
        {
            if (!txtDisplay.Text.Contains("."))
                txtDisplay.Text += ".";
        }
        else
        {
            if (nuevoNumero)
            {
                txtDisplay.Text = texto;
                nuevoNumero = false;
            }
            else
            {
                if (txtDisplay.Text == "0" && texto != ".")
                    txtDisplay.Text = texto;
                else
                    txtDisplay.Text += texto;
            }
        }
    }
}
