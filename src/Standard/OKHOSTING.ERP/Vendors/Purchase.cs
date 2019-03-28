using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.New.Vendors
{
	/// <summary>
	/// A purchase that the company made to a vendor
	/// </summary>
	public class Purchase : Invoice
	{
		public Purchase()
		{
			InvoiceType = ERP.New.InvoiceType.Purchase;
		}

		[RequiredValidator]
		public Vendor Vendor
		{
			get;
			set;
		}
	}
}