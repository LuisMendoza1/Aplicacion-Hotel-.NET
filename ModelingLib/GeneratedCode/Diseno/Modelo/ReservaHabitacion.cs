﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Diseno.Modelo
{
	using Diseno.Controlador;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class ReservaHabitacion
	{
		public virtual int idReservaHabitacion
		{
			get;
			set;
		}

		public virtual Reserva Reserva
		{
			get;
			set;
		}

		public virtual Habitacion Habitacion
		{
			get;
			set;
		}

		public virtual void Search(DateTime fechaInicio, DateTime fechaFin)
		{
			throw new System.NotImplementedException();
		}

		public virtual void saveReservaHabitacion(Reserva reserva, Habitacion habitacion)
		{
			throw new System.NotImplementedException();
		}

		public virtual Reserva getReserva()
		{
			throw new System.NotImplementedException();
		}

		public virtual void getHabitaciones()
		{
			throw new System.NotImplementedException();
		}

		public virtual void getHabitacionesReservas(DateTime fechaInicio, DateTime fechaFin)
		{
			throw new System.NotImplementedException();
		}

		public virtual void setReservaHabitacion()
		{
			throw new System.NotImplementedException();
		}

	}
}

