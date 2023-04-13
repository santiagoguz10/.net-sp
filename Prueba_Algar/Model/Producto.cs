using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prueba_Algar.Model
{
    public class Producto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public int ValorProducto { get; set; }
    }
}
