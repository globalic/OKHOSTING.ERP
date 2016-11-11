using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// An invoice
	/// </summary>
	/// <remarks>An invoice can be a customer or a vendor invoice and represents a bussines transaction, a sale or a purchase</remarks>
	public abstract class Invoice
	{
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
			get
			{
				return Items.Sum(i => i.Subtotal);
			}
		}

		/// <summary>
		/// Total sum of sales taxes
		/// </summary>
		public decimal Tax
		{
			get
			{
				return Items.Sum(i => i.Tax);
			}
		}

		/// <summary>
		/// Total sum of sales items, including discounts and taxes
		/// </summary>
		public decimal Total
		{
			get
			{
				return Items.Sum(i=> i.Total);
			}
		}

		/// <summary>
		/// Ammount paid so far
		/// </summary>
		public decimal Paid
		{
			get
			{
				return Payments.Sum(p => p.Amount);
			}
		}

		/// <summary>
		/// Whether the invoice is fully paid or not
		/// </summary>
		public decimal Balance
		{
			get
			{
				return Total - Paid;
			}
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

		[RequiredValidator]
		public InvoiceCategory Category
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
	}
}