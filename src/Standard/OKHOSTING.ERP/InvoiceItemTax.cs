using System;
using OKHOSTING.ERP.New.Finances;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

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

		/// <summary>
		/// Calculates the amount of tax
		/// </summary>
		public void OnBeforeInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeInsert(sender, eventArgs);

			CalculateAmount();
		}

		/// <summary>
		/// Calculates the amount of tax
		/// </summary>
		public void OnBeforeUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeUpdate(sender, eventArgs);

			CalculateAmount();
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// </summary>
		public void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			//re-calculate invoice item totals
			sender.Select(Item);
			Item.CalculateTotals();
			sender.Update(Item);
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// </summary>
		public void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate invoice item totals
			sender.Select(Item);
			Item.CalculateTotals();
			sender.Update(Item);
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// </summary>
		public void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate invoice item totals
			sender.Select(Item);
			Item.CalculateTotals();
			sender.Update(Item);
		}
	}
}