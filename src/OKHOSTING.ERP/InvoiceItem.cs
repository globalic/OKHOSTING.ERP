using System;
using System.Linq;
using OKHOSTING.ERP.Production;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// An item that belongs to an invoice
	/// </summary>
	public class InvoiceItem
	{
		[RequiredValidator]
		public Product Product
		{
			get;
			set;
		}

		/// <summary>
		/// Number of items sold
		/// </summary>
		[RequiredValidator]
		public decimal Quantity
		{
			get;
			set;
		}

		/// <summary>
		/// Individual product price
		/// </summary>
		[RequiredValidator]
		public decimal Price
		{
			get;
			set;
		}

		/// <summary>
		/// Individual product discount
		/// </summary>
		[RequiredValidator]
		public decimal Discount
		{
			get;
			set;
		}

		/// <summary>
		/// Description for this item
		/// </summary>
		[StringLengthValidator(100)]
		[RequiredValidator]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Invoice which this item belongs to
		/// </summary>
		[RequiredValidator]
		public Invoice Invoice
		{
			get;
			set;
		}

		/// <summary>
		/// Product instance that is being sold/bought in the current Invoice (optional)
		/// </summary>
		public ProductInstance ProductInstance
		{
			get;
			set;
		}

		#region Calculated fields

		/// <summary>
		/// Total sum of sales items, including discounts but not taxes
		/// </summary>
		public decimal Subtotal
		{
			get
			{
				return (Quantity * Price) - Discount;
			}
		}

		/// <summary>
		/// Total ammount of taxes
		/// </summary>
		public decimal Tax
		{
			get
			{
				return Convert.ToDecimal(Taxes.Sum(a => a.Amount));
			}
		}

		/// <summary>
		/// Total ammount of the sale, including discounts and taxes
		/// </summary>
		public decimal Total
		{
			get
			{
				return Subtotal + Tax;
			}
		}

		#endregion

		public ICollection<InvoiceItemTax> Taxes
		{
			get;
			set;
		}

	}
}