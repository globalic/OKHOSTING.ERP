using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.New.Vendors
{
	/// <summary>
	/// A purchase that the company made to a vendor
	/// </summary>
	public class Purchase : Invoice
	{
		public Purchase()
		{
			InvoiceType = ERP.New.InvoiceType.Purchase;
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
		public void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			//re-calculate vendor balance
			sender.Select(Vendor);
			Vendor.CalculateBalance();
			sender.Update(Vendor);
		}

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		public void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate vendor balance
			sender.Select(Vendor);
			Vendor.CalculateBalance();
			sender.Update(Vendor);
		}

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		public void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate vendor balance
			sender.Select(Vendor);
			Vendor.CalculateBalance();
			sender.Update(Vendor);
		}
	}
}