using Panaderia.Enums;

namespace Panaderia.Models
{
    public class Producto
    {
        private string _nombre;
        private double _precio;
        private TipoProducto _tipo;

        public string Nombre => _nombre;
        public double Precio => _precio;
        public TipoProducto Tipo => _tipo;

        public Producto(string nombre, double precio, TipoProducto tipo)
        {
            _nombre = nombre;
            _precio = precio;
            _tipo = tipo;
        }

        public override string ToString()
        {
            return $"{Nombre}, {Precio}, {Tipo}";
        }
    }
}
