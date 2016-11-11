using System;
using OKHOSTING.ERP.Finances;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// Tax applied to an invoice item
	/// </summary>
	public class InvoiceItemTax
	{
		/// <summary>
		/// Invoice item that generated this tax
		/// </summary>
		[RequiredValidator]
		public InvoiceItem Item
		{
			get;
			set;
		}

		/// <summary>
		/// Tax being applied to the invoice item
		/// </summary>
		[RequiredValidator]
		public Tax Tax
		{
			get;
			set;
		}

		/// <summary>
		/// Ammount being charged as tax
		/// </summary>
		public decimal? Amount
		{
			get 
			{
				return Item?.Subtotal * Tax?.Rate / 100;
			}
		}
	}
}