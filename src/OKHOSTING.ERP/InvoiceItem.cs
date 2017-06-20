using System;
using System.Linq;
using OKHOSTING.ERP.Production;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// An item that belongs to an invoice
	/// </summary>
	public class InvoiceItem : PersistentClass<Guid>
	{
		Product _Product;

		[RequiredValidator]
		public Product Product
		{
			get
			{
				return _Product;
			}
			set
			{
				_Product = value;
				
				if (value != null && Price == 0)
				{
					Price = value.Price;
				}
			}
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
		public decimal Price
		{
			get;
			set;
		}

		/// <summary>
		/// Individual product discount
		/// </summary>
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
			get;
			set;
		}

		/// <summary>
		/// Total ammount of taxes
		/// </summary>
		public decimal Tax
		{
			get;
			set;
		}

		/// <summary>
		/// Total ammount of the sale, including discounts and taxes
		/// </summary>
		public decimal Total
		{
			get;
			set;
		}

		#endregion

		public ICollection<InvoiceItemTax> Taxes
		{
			get;
			set;
		}

		/// <summary>
		/// Total ammount of the sale, including discounts but not taxes
		/// </summary>
		/// <value>Quantity * Price</value>
		private void CalculateSubtotal()
		{
			Subtotal = (this.Quantity * this.Price) - this.Discount;
		}

		/// <summary>
		/// Total ammount of taxes
		/// </summary>
		private void CalculateTax()
		{
			Tax = 0;

			//if no taxes are defined, return
			if (Taxes == null) return;

			foreach (InvoiceItemTax tax in this.Taxes)
			{
				tax.CalculateAmount();
				Tax += tax.Amount.Value;
			}
		}

		/// <summary>
		/// Total ammount of the sale, including taxes and discount
		/// </summary>
		/// <value>PriceTotal + TaxTotal</value>
		private void CalculateTotal()
		{
			Total = Subtotal + Tax;
		}

		/// <summary>
		/// Performs calculations for subtotal, tax, total, paid and balance
		/// </summary>
		public void CalculateTotals()
		{
			//if necesary, load taxes
			if (IsSaved && (Taxes == null || Taxes.Count == 0))
			{
				using (var db = DataBase.CreateDataBase())
				{
					db.LoadCollection(this, i=> i.Taxes);
					
					foreach (var t in Taxes)
					{
						db.Select(t.Tax);
					}
				}
			}

			CalculateSubtotal();
			CalculateTax();
			CalculateTotal();
		}

		public override string ToString()
		{
			return Description;
		}

		/// <summary>
		/// Deletes all taxes of this item
		/// </summary>
		protected override void OnBeforeDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnBeforeDelete(sender, eventArgs);

			sender.LoadCollection(this, i => i.Taxes);

			foreach (var tax in Taxes)
			{
				sender.Delete(tax);
			}
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// </summary>
		protected override void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterDelete(sender, eventArgs);

			//re-calculate invoice totals
			Invoice.SelectOnce();
			Invoice.CalculateTotals();
			Invoice.Update();
		}

		/// <summary>
		/// Calculates totals
		/// </summary>
		protected override void OnBeforeInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//get price from product
			if (Price == 0)
			{
				Price = Product.Price;
			}

			//insert item taxes according to product
			if (Taxes == null || !Taxes.Any())
			{
				Taxes = new List<ERP.InvoiceItemTax>();

				sender.Select(Product);
				ERP.Finances.TaxGroup group = null;

				if (Invoice is Customers.Sale || Invoice is Customers.Quote)
				{
					group = Product.SaleTaxes;
				}
				else if (Invoice is Vendors.Purchase)
				{
					group = Product.SaleTaxes;
				}

				sender.LoadCollection(group, g => g.Taxes);

				CalculateSubtotal();

				foreach (var t in group.Taxes)
				{
					sender.Select(t.Tax);
					ERP.InvoiceItemTax itemTax = new ERP.InvoiceItemTax();
					itemTax.Item = this;
					itemTax.Tax = t.Tax;
					itemTax.CalculateAmount();

					Taxes.Add(itemTax);
				}
			}

			//calculate items totals
			CalculateTotals();

			base.OnBeforeInsert(sender, eventArgs);
		}

		/// <summary>
		/// Inserts all items taxes along with the current item and recalculates item's and invoice's totals
		/// </summary>
		protected override void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterInsert(sender, eventArgs);

			//foreach (var t in Taxes)
			//{
			//	sender.Insert(t);
			//}

			Invoice.SelectOnce();
			Invoice.CalculateTotals();
			sender.Update(Invoice);
		}

		/// <summary>
		/// Re-calculates totals
		/// </summary>
		protected override void OnBeforeUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnBeforeUpdate(sender, eventArgs);
			CalculateTotals();
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// </summary>
		protected override void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterUpdate(sender, eventArgs);

			//re-calculate invoice totals
			Invoice.SelectOnce();
			Invoice.CalculateTotals();
			Invoice.Update();
		}
	}
}