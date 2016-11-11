using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Accounting
{
	/// <summary>
	/// Type of account used in traditional finance accounting
	/// </summary>
	public class DoubleEntryAccount: Account
	{
		/// <summary>
		/// If true, this is an Active account, otherwise, this is a Passive account
		/// </summary>
		public bool IsActive
		{
			get { return GetPropertyValue<bool>("IsActive"); }
			set { SetPropertyValue("IsActive", value); }
		}
	}
}