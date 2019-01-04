using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion
{
	/// <summary>
	/// Representa los tipos de impuestos que se pueden declarar en una factura, ya sea como retenciones o como traslados
		/// <para xml:lang="en">
		///  Represents types of taxes to use for an invoice, as retentions or transfers
		/// </para>
	/// </summary>
	public enum Impuesto
	{
		IVA = 1,
		ISR = 2,
		IEPS = 3,
	}
}