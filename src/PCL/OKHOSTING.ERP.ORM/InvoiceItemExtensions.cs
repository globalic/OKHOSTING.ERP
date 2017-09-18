using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM.Model;

namespace OKHOSTING.ERP.ORM
{
	public static class InvoiceItemExtensions
	{
		
		/// <summary>
		/// Deletes all taxes of this item
		/// </summary>
		public static void OnBeforeDelete(this InvoiceItem item, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeDelete(sender, eventArgs);

			foreach (var tax in item.Taxes)
			{
				sender.Delete(tax);
			}
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// </summary>
		public static void OnAfterDelete(this InvoiceItem item, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate invoice totals
			item.Invoice.Select();
			item.Invoice.CalculateTotals();
			item.Invoice.Update();
		}

		/// <summary>
		/// Calculates totals
		/// </summary>
		public static void OnBeforeInsert(this InvoiceItem item, DataBase sender, OperationEventArgs eventArgs)
		{
			//get price from product
			if (item.Price == 0)
			{
				item.Price = item.Product.Price;
			}
			
			//base.OnBeforeInsert(sender, eventArgs);
		}

		/// <summary>
		/// Inserts all items taxes along with the current item and recalculates item's and invoice's totals
		/// </summary>
		public static void OnAfterInsert(this InvoiceItem item, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			//insert item taxes according to product
			if (item.Taxes == null || !item.Taxes.Any())
			{
				item.Taxes = new List<InvoiceItemTax>();

				sender.Select(item.Product);

				Finances.TaxGroup group = null;

				if (item.Invoice is Customers.Sale)
				{
					group = item.Product.SaleTaxes;
				}
				else if (item.Invoice is Vendors.Purchase)
				{
					group = item.Product.SaleTaxes;
				}

				group.Select();
				sender.LoadCollection(group, g => g.Taxes);

				foreach (var t in group.Taxes)
				{
					t.Tax.Select();

					InvoiceItemTax itemTax = new InvoiceItemTax();
					itemTax.Item = item;
					itemTax.Tax = t.Tax;
					itemTax.CalculateAmount();

					item.Taxes.Add(itemTax);

					itemTax.Insert();
				}
			}

			item.Invoice.Select();
			item.Invoice.CalculateTotals();
			item.Invoice.Update();
		}

		/// <summary>
		/// Re-calculates totals
		/// </summary>
		public static void OnBeforeUpdate(this InvoiceItem item, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeUpdate(sender, eventArgs);
			item.CalculateTotals();
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// </summary>
		public static void OnAfterUpdate(this InvoiceItem item, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate invoice totals
			item.Invoice.Select();
			item.Invoice.CalculateTotals();
			item.Invoice.Update();
		}

	}
}