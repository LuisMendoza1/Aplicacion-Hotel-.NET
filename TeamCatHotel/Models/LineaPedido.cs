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
	public class LineaPedido
	{
        [Required]
        [Range(1, 1000)]
        public virtual int cantidad
		{
			get;
			set;
		}
        [Required]
        [Range(1, 1000)]
        [DataType(DataType.Currency)]
        public virtual float precio
		{
			get;
			set;
		}
        [Key]
        public virtual int idLineaPedido
		{
			get;
			set;
		}
        [ForeignKey("idProducto")]
        public virtual Producto producto
		{
			get;
			set;
		}
        [ForeignKey("idPedido")]
        public virtual Pedido pedido
		{
			get;
			set;
		}

	}
}
