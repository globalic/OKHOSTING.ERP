using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion
{
	/// <summary>
	/// Representa un impuesto retenido (que representa una obligación de pago para el receptor) en una factura
		/// <para xml:lang="en">
		///  Represents a detained tax (that must be paid by the receiver) in an invoice
		/// </para>
	/// </summary>
	public struct Retencion
	{
		public Impuesto Impuesto;
		public decimal Importe;
	}
}