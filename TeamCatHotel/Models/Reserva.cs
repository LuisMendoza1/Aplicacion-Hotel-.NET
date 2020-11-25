﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo se genero mediante una herramienta.
//     Los cambios del archivo se perderan si se regenera el codigo.
// </auto-generated>
//------------------------------------------------------------------------------

//using Diseno.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TeamCatHotel.Models
{
    public class Reserva
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

        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTime fechaRealizacion
		{
			get;
			set;
		}

        [StringLength(150, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public virtual string comentarios
		{
			get;
			set;
		}

        [Required]
		public virtual int regimenComida
		{
			get;
			set;
		}

        [Required]
		public virtual int estado
		{
			get;
			set;
		}

        [Key]
		public virtual int idReserva
		{
			get;
			set;
		}

        [ForeignKey("idDescuento")]
        public virtual Descuento Descuento
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

        [ForeignKey("idCliente")]
        public virtual Cliente Cliente
		{
			get;
			set;
		}

		public virtual ICollection<ReservaHabitacion> ReservaHabitacion
		{
			get;
			set;
		}

	}
}

