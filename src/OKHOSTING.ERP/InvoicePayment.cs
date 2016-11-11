using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A payment made for an invoice
	/// </summary>
	public class InvoicePayment
	{
		[RequiredValidator]
		public Invoice Invoice
		{
			get;
			set;
		}

		[RequiredValidator]
		public decimal Amount
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime Date
		{
			get;
			set;
		}

		[RequiredValidator]
		public PaymentMethod Method
		{
			get;
			set;
		}
	}
}