using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCatHotel.Models
{
    public class CreateProductoProveedor : Controller
    {
        // GET: /<controller>/
        public virtual float precio
        {
            get;
            set;
        }
        public virtual int idProducto
        {
            get;
            set;
        }
        public virtual int idProveedor
        {
            get;
            set;
        }
        public IEnumerable<Producto> Productos { get; set; }

        public IEnumerable<Proveedor> proveedores { get; set; }
    }
}
