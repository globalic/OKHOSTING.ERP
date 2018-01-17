using System;
using OKHOSTING.ERP.New.Finances;
using OKHOSTING.Data.Validation;



namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// Tax applied to an invoice item
	/// </summary>
	public class InvoiceItemTax
	{
		public Guid Id { get; set; }

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
			get;
			set;
		}

		/// <summary>
		/// Calculates the amount of the tax
		/// </summary>
		public void CalculateAmount()
		{
			//Tax?.SelectOnce();
			//Item?.SelectOnce();
			Amount = Tax?.GetTaxFor(Item.Subtotal);
		}
	}
}