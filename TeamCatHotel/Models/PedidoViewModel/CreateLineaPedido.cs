using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCatHotel.Models
{
    public class CreateLineaPedido : Controller
    {
        // GET: /<controller>/

        public virtual int cantidad
        {
            get;
            set;
        }
     
        public virtual float precio
        {
            get;
            set;
        }
     
        public virtual int idLineaPedido
        {
            get;
            set;
        }
       public int idProducto
        {
            get;
            set;
        }
        public int idPedido
        {
            get;
            set;
        }
        public IEnumerable<Producto> Productos { get; set; }

        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
