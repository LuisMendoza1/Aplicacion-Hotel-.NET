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

	public class Pedido
	{
		public virtual DateTime fechaEmision
		{
			get;
			set;
		}

		public virtual DateTime fechaRecepcion
		{
			get;
			set;
		}

		public virtual int idPedido
		{
			get;
			set;
		}

		public virtual IEnumerable<LineaPedido> LineasPedido
		{
			get;
			set;
		}

		public virtual Proveedor Proveedor
		{
			get;
			set;
		}

		public virtual void <<create>>(Pedido pedido)
		{
			throw new System.NotImplementedException();
		}

		public virtual void setPedido(Producto producto, int cantidad, Proveedor proveedor)
		{
			throw new System.NotImplementedException();
		}

		public virtual void <<create>>()
		{
			throw new System.NotImplementedException();
		}

	}
}

