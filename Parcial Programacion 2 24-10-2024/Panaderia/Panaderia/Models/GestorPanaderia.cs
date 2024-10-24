using Panaderia.Enums;
using System.Xml.Linq;

namespace Panaderia.Models
{
    public static class GestorPanaderia
    {
        public static List<Producto> ListaProductos = new();
        public static readonly string ArchivoProductos = "productos.txt";

        public static void AgregarProducto(Producto producto)
        {
            var produc = ListaProductos.Find(p => p.Nombre == producto.Nombre);
            if (produc != null)
            {
                Console.WriteLine("Este producto ya existe.");
                return;
            }

            ListaProductos.Add(producto);
            Console.WriteLine("Producto agregado correctamente!");
            GuardarDatos(producto, true);
        }

        public static void EliminarProducto(string nombre)
        {
            var producto = ListaProductos.Find(p => p.Nombre == nombre);
            if (ListaProductos.Remove(producto))
            {
                Console.WriteLine("Producto eliminado correctamente!");
                GuardarDatos(ListaProductos, false);
            }
            else
            {
                Console.WriteLine("Nombre del producto no encontrado.");
            }
        }

        public static void ModificarProducto(TipoProducto nuevoTipo, double nuevoPrecio, string nombre)
        {
            var producto = ListaProductos.Find(p => p.Nombre == nombre);
            if (!ListaProductos.Remove(producto))
            {
                Console.WriteLine("Nombre del producto no encontrado.");
                return;
            }

            ListaProductos.Remove(producto);

            var nuevoProducto = new Producto(nombre, nuevoPrecio, nuevoTipo);
            ListaProductos.Add(nuevoProducto);
            Console.WriteLine("Producto Modificado correctamente");
            GuardarDatos(ListaProductos, false);
        }

        public static void MostrarProductos()
        {
            foreach (var producto in ListaProductos)
            {
                Console.WriteLine(producto);
            }
        }

        public static void GuardarDatos<T>(T entidad, bool append) where T : class
        {
            using StreamWriter writer = new StreamWriter(ArchivoProductos, append);
            writer.WriteLine(entidad);
        }

        public static void GuardarDatos<T>(List<T> entidades, bool append) where T : class
        {
            using StreamWriter writer = new StreamWriter(ArchivoProductos, append);
            foreach (var entidad in entidades)
            {
                writer.WriteLine(entidad);
            }
        }

        public static List<string> ConvertirArchivoEnLista(string archivo)
        {
            List<string> lineas = new();
            if (File.Exists(archivo))
            {
                foreach (var linea in File.ReadAllLines(archivo))
                {
                    lineas.Add(linea);
                }
            }
            return lineas;
        }

        public static void CargarDatos(string archivo, string separador)
        {
            List<string> lineas = ConvertirArchivoEnLista(archivo);

            if (lineas.Count <= 0)
            {
                return;
            }

            foreach (var linea in lineas)
            {
                string[] datos = linea.Split(separador);

                try
                {
                    string nombre = datos[0];
                    double precio = double.Parse(datos[1]);
                    TipoProducto tipo = (TipoProducto)Enum.Parse(typeof(TipoProducto), datos[2]);

                    Producto producto = new Producto(nombre, precio, tipo);
                    ListaProductos.Add(producto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar datos desde el archivo {archivo} en la linea {lineas.IndexOf(linea)} : {ex.Message}");
                }
            }
        }
    }
}
