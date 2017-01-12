using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A payment made for an invoice
	/// </summary>
	public class InvoicePayment : PersistentClass<Guid>
	{
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
		/// Inserts all items taxes along with the current item and recalculates item's and invoice's totals
		/// </summary>
		protected override void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterInsert(sender, eventArgs);

			//re-calculate invoice totals
			Invoice.SelectOnce();
			Invoice.CalculateTotals();
			Invoice.Update();
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