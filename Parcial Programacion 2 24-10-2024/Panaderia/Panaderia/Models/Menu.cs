using Panaderia.Enums;

namespace Panaderia.Models
{
    public static class Menu
    {
        static List<Action> Acciones = new List<Action>
        {
            AgregarProducto,
            EliminarProducto,
            ModificarProducto,
            MostrarProductos,
            CalcularTotal
        };

        public static void MostrarMenu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n----- Menú -----\n" +
                    "1. Agregar Producto\n" +
                    "2. Eliminar Producto\n" +
                    "3. Modificar Producto\n" +
                    "4. Mostrar Productos\n" +
                    "5. Calcular Total\n" +
                    "6. Salir\n");
                Console.Write("Seleccione una opcion: ");
                string opcion = Console.ReadLine();

                if (int.TryParse(opcion, out int indice) && indice >= 1 && indice <= Acciones.Count + 1)
                {
                    if (indice == Acciones.Count + 1)
                    {
                        Console.WriteLine("Saliendo...");
                        salir = true;
                    }
                    else
                    {
                        Acciones[indice - 1].Invoke();
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese una opción válida.");
                }
            }
        }

        public static void AgregarProducto()
        {
            Console.Write("Ingrese el nombre del producto: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese el precio del producto: ");
            double precio = double.Parse(Console.ReadLine());

            Console.WriteLine("Seleccione el tipo del producto: ");
            foreach (var tipo in Enum.GetValues(typeof(TipoProducto)))
            {
                Console.WriteLine($"{(int)tipo}. {tipo}");
            }
            string opcion = Console.ReadLine();

            TipoProducto tipoSeleccionado = (TipoProducto)Enum.Parse(typeof(TipoProducto), opcion);

            Producto producto = new Producto(nombre, precio, tipoSeleccionado);
            GestorPanaderia.AgregarProducto(producto);
        }

        public static void EliminarProducto()
        {
            if (GestorPanaderia.ListaProductos.Count == 0)
            {
                Console.WriteLine("Lista de productos vacía.");
                return;
            }

            Console.Write("Ingrese el nombre del producto que quiere eliminar: ");
            string nombre = Console.ReadLine();

            GestorPanaderia.EliminarProducto(nombre);
        }

        public static void ModificarProducto()
        {
            if (GestorPanaderia.ListaProductos.Count == 0)
            {
                Console.WriteLine("Lista de productos vacía.");
                return;
            }

            Console.Write("Ingrese el nombre del producto que quiere modificar: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo precio del producto: ");
            double nuevoPrecio = double.Parse(Console.ReadLine());

            Console.WriteLine("Seleccione el nuevo Tipo del producto: ");
            foreach (var tipo in Enum.GetValues(typeof(TipoProducto)))
            {
                Console.WriteLine($"{(int)tipo}. {tipo}");
            }
            string opcion = Console.ReadLine();

            TipoProducto tipoSeleccionado = (TipoProducto)Enum.Parse(typeof(TipoProducto), opcion);

            GestorPanaderia.ModificarProducto(tipoSeleccionado, nuevoPrecio, nombre);
        }

        public static void MostrarProductos()
        {
            if(GestorPanaderia.ListaProductos.Count == 0)
            {
                Console.WriteLine("Lista de productos vacía.");
                return;
            }

            GestorPanaderia.MostrarProductos();
        }

        public static void CalcularTotal()
        {
            if (GestorPanaderia.ListaProductos.Count == 0)
            {
                Console.WriteLine("Lista de productos vacía.");
                return;
            }

            double total = 0;
            foreach(var producto in GestorPanaderia.ListaProductos)
            {
                total += producto.Precio;
            }

            Console.WriteLine($"El Total de todos los productos en inventario es {total:C}");
        }
    }
}
