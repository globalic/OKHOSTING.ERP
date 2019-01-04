using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion
{
	/// <summary>
	/// Empresa que puede ser usada como Emisor o Receptor dentro de una factura
		/// <para xml:lang="en">
		/// Company that can be used as sender or receiver in an invoice
		/// </para>
	/// </summary>
	[Serializable]
	public struct Empresa
	{
		/// <summary>
		/// Razón Social de la empresa
		/// <para xml:lang="en">
		/// Business name of the company
		/// </para>
		/// </summary>
		public string Nombre;
		
		/// <summary>
		/// Registro Federal de Constribuyentes de la empresa, sin guiones ni espacios, todo con mayúsculas
		/// <para xml:lang="en">
		/// RFC of the company without scores or blankspaces. Use Uppercase. 
		/// </para>
		/// </summary>
		public string RFC;
		
		/// <summary>
		/// Nodo requerido para incorporar los regímenes en los que tributa el contribuyente emisor. Puede contener más de un régimen.
		/// </summary>
		/// <para xml:lang="en">
		/// Node required for adding sender tributaries regimes. Can contain more than one. 
		/// </para>
		/// <example>
		/// Sociedad Civil, Sociedad Anonima, Persona Fisica con Actividad Empresarial
		/// </example>
		public string RegimenFiscal;
	}
}