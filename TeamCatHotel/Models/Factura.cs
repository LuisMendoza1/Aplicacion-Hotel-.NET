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
    public class Factura
	{
        [DataType(DataType.DateTime)]
		public virtual DateTime fechaEmision
		{
			get;
			set;
		}

        [DataType(DataType.DateTime)]
        public virtual DateTime fechaPago
		{
			get;
			set;
		}

        [Required]
		public virtual bool abono
		{
			get;
			set;
		}

        [Key]
		public virtual int idFactura
		{
			get;
			set;
		}

		public virtual ICollection<LineaFactura> LineasFactura
		{
			get;
			set;
		}

        [ForeignKey("idReserva")]
		public virtual Reserva Reserva
		{
			get;
			set;
		}

	}
}
