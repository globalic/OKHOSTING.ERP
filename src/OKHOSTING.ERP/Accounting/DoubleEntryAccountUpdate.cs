using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Accounting
{
	public class DoubleEntryAccountUpdate: AccountUpdate
	{
		/// <summary>
		/// The account that was also affected by the update, in the opossite way to the Account
		/// </summary>
		public Account CounterAccount
		{
			get { return GetPropertyValue<Account>("CounterAccount"); }
			set { SetPropertyValue("CounterAccount", value); }
		}
	}
}