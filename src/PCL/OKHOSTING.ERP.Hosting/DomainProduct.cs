using System;
using OKHOSTING.ERP.New.Production;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Hosting
{
	/// <summary>
	/// A product for selling domain names
	/// <para xml:lang="es">
	/// Un producto para vender nombres de dominio
	/// </para>
	/// </summary>
	public class DomainProduct: SubscriptionProduct
	{
		/// <summary>
		/// The top level domain (extension) for the domain name
		/// <para xml:lang="es">
		/// El nivel mas alto de dominio (extención) para el nombre de dominio
		/// </para>
		/// </summary>
		/// <example>com, net, org, com.mx, mx</example>
		[RequiredValidator]
		[StringLengthValidator(10)]
		public string Tld
		{
			get;
			set;
		}
	}
}