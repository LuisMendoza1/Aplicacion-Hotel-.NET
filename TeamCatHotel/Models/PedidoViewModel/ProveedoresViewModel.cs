using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCatHotel.Models.PedidoViewModel
{
    public class ProveedoresViewModel
    {
        public IEnumerable<Proveedor> proveedores { get; set; }

        public int idProveedor { get; set; }

        public Boolean noProductosSeleccionados { get; set; }

        public Boolean noPositivo { get; set; }

        public Boolean mayorCien { get; set; }
    }
}
