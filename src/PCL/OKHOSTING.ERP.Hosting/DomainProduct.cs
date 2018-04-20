using System;
using OKHOSTING.ERP.New.Production;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Hosting
{
	/// <summary>
	/// A product for selling domain names
	/// </summary>
	public class DomainProduct: SubscriptionProduct
	{
		/// <summary>
		/// The top level domain (extension) for the domain name
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