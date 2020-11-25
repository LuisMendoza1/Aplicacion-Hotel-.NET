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

    public class LineaFactura
	{
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, float.MaxValue)]
		public virtual float precio
		{
			get;
			set;
		}

        [Key]
		public virtual int idLineaFactura
		{
			get;
			set;
		}

        [ForeignKey("idFactura")]
		public virtual Factura Factura
		{
			get;
			set;
		}

        [ForeignKey("idReservaServicio")]
		public virtual ReservaServicio ReservaServicio
		{
			get;
			set;
		}
	}
}
