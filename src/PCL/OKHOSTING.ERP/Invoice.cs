using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.Data.Validation;



namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// An invoice
	/// </summary>
	/// <remarks>An invoice can be a customer or a vendor invoice and represents a bussines transaction, a sale or a purchase</remarks>
	public class Invoice
	{
		public Guid Id { get; set; }

		[StringLengthValidator(50)]
		public string AuxId
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

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Notes
		{
			get;
			set; 
		}

		/// <summary>
		/// Total sum of sales items, including discounts but not taxes
		/// </summary>
		public decimal Subtotal
		{
			get;
			set;
		}

		/// <summary>
		/// Total sum of sales taxes
		/// </summary>
		public decimal Tax
		{
			get;
			set;
		}

		/// <summary>
		/// Total sum of sales items, including discounts and taxes
		/// </summary>
		public decimal Total
		{
			get;
			set;
		}

		/// <summary>
		/// Whether the invoice is fully paid or not
		/// </summary>
		public decimal Balance
		{
			get;
			set;
		}

		/// <summary>
		/// Whether this is a sale or a purchase
		/// </summary>
		[RequiredValidator]
		public InvoiceType InvoiceType
		{
			get;
			set;
		}

		public InvoiceCategory Category
		{
			get;
			set;
		}

		public Production.Task Task
		{
			get;
			set;
		}

		public InvoiceStatus Status
		{
			get;
			set;
		}

		#region Collections

		/// <summary>
		/// List of items included in this invoice
		/// </summary>
		public ICollection<InvoiceItem> Items
		{
			get;
			set;
		}

		/// <summary>
		/// List of payments made for this invoice
		/// </summary>
		public ICollection<InvoicePayment> Payments
		{
			get;
			set;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Total ammount of the invoice, including discounts but not taxes
		/// </summary>
		private void CalculateSubtotal()
		{
			Subtotal = 0;

			foreach (InvoiceItem item in Items)
			{
				item.CalculateTotals();
				Subtotal += item.Subtotal;
			}
		}

		/// <summary>
		/// Total ammount of taxes
		/// </summary>
		private void CalculateTax()
		{
			Tax = 0;

			foreach (InvoiceItem item in Items)
			{
				Tax += item.Tax;
			}
		}

		/// <summary>
		/// Total ammount of the sale, including taxes and discount
		/// </summary>
		private void CalculateTotal()
		{
			//Total = decimal.Round(Subtotal + Tax, 1);
			Total = Subtotal + Tax;

			//round subtotal
			//if (Total != Subtotal + Tax)
			//{
			//	Subtotal = Total - Tax;
			//}
		}

		/// <summary>
		/// Calculates invoice balance
		/// </summary>
		private void CalculateBalance()
		{
			Balance = Total - Payments.Sum(p => p.Amount);
		}

		/// <summary>
		/// Performs calculations for subtotal, tax, total, paid and balance
		/// </summary>
		public void CalculateTotals()
		{
			//calculate this invoice's totals
			CalculateSubtotal();
			CalculateTax();
			CalculateTotal();
			CalculateBalance();
		}

		/// <summary>
		/// Returns the AuxId or the Id
		/// </summary>
		public override string ToString()
		{
			if (AuxId != null && AuxId != "")
				return AuxId.ToString();
			else
				return Id.ToString();
		}

		#endregion
	}
}