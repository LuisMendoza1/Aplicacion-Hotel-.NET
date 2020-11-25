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
    public class ReservaServicio
	{
        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTime fechaInicio
		{
			get;
			set;
		}
        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTime fechaFin
		{
			get;
			set;
		}
        [Key]
		public virtual int idReservaServicio
		{
			get;
			set;
		}

        [ForeignKey("idServicio")]
        public virtual Servicio Servicio
		{
			get;
			set;
		}

        [ForeignKey("idLineaFactura")]
        public virtual LineaFactura LineaFactura
		{
			get;
			set;
		}

        [ForeignKey("numero")]
        public virtual Habitacion Habitacion
		{
			get;
			set;
		}

        /*public static implicit operator ReservaServicio(ReservaServicio v)
        {
            throw new NotImplementedException();
        }*/
    }
}
