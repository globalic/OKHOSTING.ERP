using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion
{
	/// <summary>
	/// Representa un concepto, producto o servicio que se factura
	/// <para xml:lang="en">
	/// Represent concept, product or service invoices 
	/// </para>
	/// </summary>
	public struct Concepto
	{
		/// <summary>
		/// Cantidad de elementos que se facturan
		/// <para xml:lang="en">
		/// Number of invoiced items
		/// </para>
		/// </summary>
		public decimal Cantidad;
		/// <summary>
		/// Unidad con la que se mide este concepto
		/// <para xml:lang="en">
		/// Unit to measure this concept
		/// </para>
		/// </summary>
		/// <example>Pieza, Kilo, Litro</example>
		public string Unidad;
		/// <summary>
		/// Código del concepto
		/// <para xml:lang="en">
		/// Concept code
		/// </para>
		/// </summary>
		public string NoIdentificacion;
		/// <summary>
		/// Descripción del concepto
		/// <para xml:lang="en">
		/// Concept description
		/// </para>
		/// </summary>
		public string Descripcion;
		/// <summary>
		/// Valor unitario o precio del concepto
		/// <para xml:lang="en">
		/// Unit concept's value or price
		/// </para>
		/// </summary>
		public decimal ValorUnitario;
		/// <summary>
		/// Importe total que se factura por el concepto
		/// <para xml:lang="en">
		/// Total invoice for this concept
		/// </para>
		/// </summary>
		/// <remarks>Debe ser equivalente a la multiplicación Cantidad x ValorUnitario
		/// <para xml:lang="en">
		/// It has to be equivalent to Cantity x unitprice
		/// </para>
		/// </remarks>
		public decimal Importe
		{
			get
			{
				return Cantidad * ValorUnitario;
			}
		}
	}
}