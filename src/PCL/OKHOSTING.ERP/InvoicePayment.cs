using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// A payment made for an invoice
	/// </summary>
	public class InvoicePayment
	{
		public Guid Id { get; set; }

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

		public override string ToString()
		{
			return Amount.ToString();
		}
	}
}