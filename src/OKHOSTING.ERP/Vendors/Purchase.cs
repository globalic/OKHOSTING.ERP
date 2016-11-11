using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Vendors
{
	/// <summary>
	/// A purchase that the company made to a vendor
	/// </summary>
	public class Purchase : Invoice
	{
		public Purchase()
		{
			InvoiceType = ERP.InvoiceType.Purchase;
		}

		[RequiredValidator]
		public Vendor Vendor
		{
			get;
			set;
		}
	}
}