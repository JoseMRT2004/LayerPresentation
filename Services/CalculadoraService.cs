namespace AppMultimedia.Services;

public class CalculadoraService
{
    public double Operar(double num1, double num2, string operador)
    {
        return operador switch
        {
            "+" => num1 + num2,
            "-" => num1 - num2,
            "*" => num1 * num2,
            "/" => num2 != 0 ? num1 / num2 : throw new DivideByZeroException(),
            _ => throw new ArgumentException("Operador inválido")
        };
    }

    public double Porcentaje(double valor)
    {
        return valor / 100;
    }

    public string CambiarSigno(string valor)
    {
        if (string.IsNullOrEmpty(valor) || valor == "0")
            return valor;
        
        return valor.StartsWith("-") ? valor.Substring(1) : "-" + valor;
    }
}
