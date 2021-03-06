using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// A payment made for an invoice
	/// </summary>
	public class InvoicePayment
	{
		public Guid Id { get; set; }

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
		public void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate invoice totals
			sender.Select(Invoice);
			Invoice.CalculateTotals();
			sender.Update(Invoice);
		}

		/// <summary>
		/// Inserts all items taxes along with the current item and recalculates item's and invoice's totals
		/// </summary>
		public void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate invoice totals
			sender.Select(Invoice);
			Invoice.CalculateTotals();
			sender.Update(Invoice);
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// </summary>
		public void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate invoice totals
			sender.Select(Invoice);
			Invoice.CalculateTotals();
			sender.Update(Invoice);
		}
	}
}