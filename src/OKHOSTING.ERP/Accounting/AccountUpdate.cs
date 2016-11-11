using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Accounting
{
	/// <summary>
	/// An AccountUpdate is a change to the value of an account at a given moment in time.
	/// It's like a snapshot of the account value in a specific moment. An account value will always be
	/// the same as it's last AccountUpdate value
	/// </summary>
	public class AccountUpdate
	{
		/// <summary>
		/// Account that this update is affecting
		/// </summary>
		[RequiredValidator]
		public Account Account
		{
            get;
            set;
		}

		/// <summary>
		/// The new value of the affected account
		/// </summary>
		[RequiredValidator]
		public decimal UpdatedValue
		{
            get;
            set;
		}

		/// <summary>
		/// Date & time when the new value was meassured and assigned to the account
		/// </summary>
		[RequiredValidator]
		public DateTime Date
		{
            get;
            set;
		}

		///// <summary>
		///// Gets the last update before the current update was created.
		///// usefull to keep track of updates in a progressive way
		///// </summary>
		///// <remarks>
		///// This value is null when the current update is the first update
		///// </remarks>
		//public AccountUpdate PreviousUpdate
		//{
		//	get 
		//	{
		//		int index = Account.History.IndexOf(this);

		//		//search for the previous update
		//		if (Account.History.Count > index + 1)
		//		{
		//			return Account.History[index + 1];
		//		}
		//		else
		//		{
		//			return null;
		//		}
		//	}
		//}

		///// <summary>
		///// Gets the difference between the current update's value and the previous update's value
		///// </summary>
		//public decimal Difference
		//{
		//	get 
		//	{
		//		if (PreviousUpdate != null)
		//		{
		//			return UpdatedValue - PreviousUpdate.UpdatedValue;
		//		}
		//		else
		//		{
		//			return UpdatedValue;
		//		}
		//	}
		//}


		//protected override void OnSaving()
		//{
		//	//An account value will always be the same as it's last AccountUpdate value
		//	if (IsNewObject)
		//	{
		//		Account.Value = UpdatedValue;
		//		Account.Save();
		//	}

		//	base.OnSaving();
		//}

		///// <summary>
		///// Overrides the parent method to save objects only when the value of the account has changed.
		///// This improves performance and saves space by skipping transactions when the value of ann account has not changed since the lastupdate
		///// </summary>
		//public new void Save()
		//{
		//	if (Account.Value != this.UpdatedValue)
		//	{
		//		base.Save();
		//	}
		//}

	}
}