using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.New.Accounting
{
	/// <summary>
	/// An account which value is automatically calculated by a child class and allows
	/// to create a statistical system that automatically refreshes every time needed
	/// </summary>
	public abstract class CalculatedAccount : Account
	{
		/// <summary>
		/// Calls CalculateCurrentValue() and updates the current account's value
		/// </summary>
		//[Action]
		//public virtual void UpdateNow()
		//{
		//	AccountUpdate update = new AccountUpdate(Session);
		//	update.Account = this;
		//	update.UpdatedValue = CalculateCurrentValue();
		//	update.Save();

		//	Value = update.UpdatedValue;
		//	Save();
		//}

		/// <summary>
		/// Calculates the account's current value
		/// </summary>
		/// <returns>
		/// A decimal containing the value that will be assigned to value property
		/// </returns>
		public abstract decimal CalculateCurrentValue();
	}
}