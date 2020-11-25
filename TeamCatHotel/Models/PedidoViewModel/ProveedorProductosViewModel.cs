using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCatHotel.Models.PedidoViewModel
{
    public class ProveedorProductosViewModel
    {
        // GET: /<controller>/
        [Display(Name = "Proveedor:")]
        public Proveedor proveedor { get; set; }

        public IEnumerable<Producto> productos { get; set; }

        [Required]       
        public virtual int[] listaCantidades { get; set; }
        public int[] IdsProductosSeleccionados { get; set; }
        public int idProveedor { get; set; }
    }
}
