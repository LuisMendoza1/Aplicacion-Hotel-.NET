﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Diseno.Modelo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class PlatoMenu
	{
		public virtual int idPlatoMenu
		{
			get;
			set;
		}

		public virtual Plato Plato
		{
			get;
			set;
		}

		public virtual Menu Menu
		{
			get;
			set;
		}

	}
}
