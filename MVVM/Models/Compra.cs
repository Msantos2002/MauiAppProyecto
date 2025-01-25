using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProyecto.MVVM.Models
{
    public class Compra
    {
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
