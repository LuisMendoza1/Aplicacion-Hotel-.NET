using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models.PedidoViewModel
{
    public class SelectProveedoresProductosViewModel
    {
        public IEnumerable<LineaPedido> lineasPedido { get; set; }

        public IEnumerable<Proveedor> proveedores { get; set; }
    }
}
