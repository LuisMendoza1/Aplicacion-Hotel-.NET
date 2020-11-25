﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo se genero mediante una herramienta.
//     Los cambios del archivo se perderan si se regenera el codigo.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TeamCatHotel.Models
{
	public class Pedido
	{
        [DataType(DataType.Date)]
        [Required]
        public virtual DateTime fechaEmision
		{
			get;
			set;
		}
        [DataType(DataType.Date)]
        public virtual DateTime fechaRecepcion
		{
			get;
			set;
		}
        [Key]
        public virtual int idPedido
		{
			get;
			set;
		}

		public virtual ICollection<LineaPedido> LineasPedido
		{
			get;
			set;
		}
        
        [ForeignKey("idProveedor")]
        public virtual Proveedor Proveedor
		{
			get;
			set;
		}

    }
}

