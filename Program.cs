using System.Windows.Forms;
using AppMultimedia.Presentation;

namespace AppMultimedia
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MenuPrincipal());
        }
    }
}
