using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models.PedidoViewModel
{
    public class ConfirmResumenViewModel
    {
        public int[] listaCantidades { get; set; }

        public int[] listaIdsProductosSeleccionados { get; set; }

        public int idProveedor { get; set; }
    }
}
