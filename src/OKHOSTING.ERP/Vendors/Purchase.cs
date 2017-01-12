using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.Vendors
{
	/// <summary>
	/// A purchase that the company made to a vendor
	/// </summary>
	public class Purchase : Invoice
	{
		public Purchase()
		{
			InvoiceType = ERP.InvoiceType.Purchase;
		}

		[RequiredValidator]
		public Vendor Vendor
		{
			get;
			set;
		}

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		protected override void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterInsert(sender, eventArgs);

			//re-calculate vendor balance
			Vendor.Select();
			Vendor.CalculateBalance();
			Vendor.Update();
		}

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		protected override void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterUpdate(sender, eventArgs);

			//re-calculate vendor balance
			Vendor.Select();
			Vendor.CalculateBalance();
			Vendor.Update();
		}

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		protected override void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterDelete(sender, eventArgs);

			//re-calculate vendor balance
			Vendor.Select();
			Vendor.CalculateBalance();
			Vendor.Update();
		}
	}
}