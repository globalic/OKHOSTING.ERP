using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion
{
	/// <summary>
	/// Domicilio que puede ser usado para domicilios fiscales o de sucursal dentro de una factura
		/// <para xml:lang="en">
		/// Address that can be use for fiscal or branch addresses within the invoice
		/// </para>
	/// </summary>
	[Serializable]
	public struct Domicilio
	{
		public string Calle;
		public string NoExterior;
		public string NoInterior;
		public string Colonia;
		public string Localidad;
		public string Referencia;
		public string Municipio;
		public string Estado;
		public string Pais;
		public string CodigoPostal;
	}
}