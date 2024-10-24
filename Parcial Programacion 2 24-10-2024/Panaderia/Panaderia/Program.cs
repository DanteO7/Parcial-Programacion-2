using Panaderia.Models;

namespace Panaderia
{
    public class Program
    {
        static void Main()
        {
            GestorPanaderia.CargarDatos(GestorPanaderia.ArchivoProductos, ", ");
            Menu.MostrarMenu();
        }
    }
}