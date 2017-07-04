using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// An invoice
	/// </summary>
	/// <remarks>An invoice can be a customer or a vendor invoice and represents a bussines transaction, a sale or a purchase</remarks>
	public class Invoice : PersistentClass<Guid>
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
		/// Ammount paid so far
		/// </summary>
		public decimal Paid
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

		[RequiredValidator]
		public InvoiceCategory Category
		{
			get;
			set;
		}

		[RequiredValidator]
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

			//if no items are defined, return
			if (Items == null) return;

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

			//if no items are defined, return
			if (Items == null) return;

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
			//    Subtotal = Total - Tax;
			//}
		}

		/// <summary>
		/// Calculates amount paid
		/// </summary>
		private void CalculatePaid()
		{
			Paid = 0;

			//if no Payments are defined, return
			if (Payments == null) return;

			foreach (InvoicePayment payment in Payments)
			{
				Paid += payment.Amount;
			}
		}

		/// <summary>
		/// Calculates invoice balance
		/// </summary>
		private void CalculateBalance()
		{
			Balance = Total - Paid;
		}

		/// <summary>
		/// Performs calculations for subtotal, tax, total, paid and balance
		/// </summary>
		public void CalculateTotals()
		{
			using (var db = DataBase.CreateDataBase())
			{
				//if necesary, load items
				if (this.IsSaved && (Items == null || Items.Count == 0))
				{
					db.LoadCollection(this, i => i.Items);
				}

				//if necesary, load payments
				if (this.IsSaved && (Payments == null || Payments.Count == 0))
				{
					db.LoadCollection(this, i => i.Payments);
				}
			}

			//calculate this invoice's totals
			CalculateSubtotal();
			CalculateTax();
			CalculateTotal();
			CalculatePaid();
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

		/// <summary>
		/// Deletes all items and payments of this invoice
		/// </summary>
		protected override void OnBeforeDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnBeforeDelete(sender, eventArgs);

			sender.LoadCollection(this, i => i.Items);

			foreach (var i in Items)
			{
				sender.Delete(i);
			}

			sender.LoadCollection(this, i => i.Payments);

			foreach (var p in Payments)
			{
				sender.Delete(p);
			}
		}

		/*
		/// <summary>
		/// Search for list and subscription items and deal with them
		/// </summary>
		protected override void  OnBeforeInsert()
		{
			base.OnBeforeInsert();

			//if no items are defined, exit
			if (Items == null) return;

			//search for subscription products
			for (int i = 0; i < Items.Count; i++)
			{
				InvoiceItem item = (InvoiceItem)Items[i];
				SubscriptionProduct subscriptionProduct = new SubscriptionProduct();
				subscriptionProduct.Id = item.Product.Id;

				if (DataBase.Current.Select(subscriptionProduct) != null)
				{
					item.Product = subscriptionProduct;

					//for subscriptions, create a SubscriptionInvoiceItem
					SubscriptionInvoiceItem subscriptionItem = new SubscriptionInvoiceItem();

					//copy values and replace current item
					item.CopyTo(subscriptionItem);
					subscriptionItem.Taxes = item.Taxes;
					foreach (InvoiceItemTax tax in subscriptionItem.Taxes)
					{
						tax.Item = subscriptionItem;
					}

					Items[i] = subscriptionItem;
				}
			}
		}
		*/

		/// <summary>
		/// Inserts all items and payments along with the current invoice
		/// </summary>
		protected override void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterInsert(sender, eventArgs);

			if (Items != null)
			{
				foreach (var i in Items)
				{
					i.Save();
				}
			}

			if (Payments != null)
			{
				foreach (var p in Payments)
				{
					p.Save();
				}
			}
		}

		#endregion
	}
}