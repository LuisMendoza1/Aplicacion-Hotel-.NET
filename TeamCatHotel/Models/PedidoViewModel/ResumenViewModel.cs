using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models.PedidoViewModel
{
    public class ResumenViewModel
    {
        public Pedido pedido { get; set; }

        [Required]
        [Range(1, 1000)]
        [DataType(DataType.Currency)]
        public double precioTotal { get; set; }

        public int idProveedor { get; set; }
    }
}
