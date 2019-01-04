using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM.Model;
using OKHOSTING.ORM;
using OKHOSTING.ERP.New.Vendors;

namespace OKHOSTING.ERP.ORM
{
	public static class PurchaseExtensions
	{

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		public static void OnAfterInsert(this Purchase purchase, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			//re-calculate vendor balance
			purchase.Vendor.Select();
			purchase.Vendor.CalculateBalance();
			purchase.Vendor.Update();
		}

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		public static void OnAfterUpdate(this Purchase purchase, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate vendor balance
			purchase.Vendor.Select();
			purchase.Vendor.CalculateBalance();
			purchase.Vendor.Update();
		}

		/// <summary>
		/// Re-calculates vendor's balance
		/// </summary>
		public static void OnAfterDelete(this Purchase purchase, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate vendor balance
			purchase.Vendor.Select();
			purchase.Vendor.CalculateBalance();
			purchase.Vendor.Update();
		}
	}
}
