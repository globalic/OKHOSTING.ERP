using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Accounting
{
	/// <summary>
	/// A projection is an estimate value that an account will have sometime in the future
	/// </summary>
	/// <remarks>
	/// Use this class to create budgets for next month or next year, create goals, etc.
	/// </remarks>
	public class AccountProjection
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Account that this projection is about
		/// </summary>
		[RequiredValidator]
		public Account Account
		{
			get;
			set;
		}

		/// <summary>
		/// The value of the account that has being projected to a date in the future
		/// </summary>
		[RequiredValidator]
		public decimal ProjectedValue
		{
			get;
			set;
		}

		/// <summary>
		/// Date & time when the projection is taking place
		/// </summary>
		[RequiredValidator]
		public DateTime Date
		{
			get;
			set;
		}

		/// <summary>
		/// Real value that this account has, usefull to compare to the projected value
		/// </summary>
		public AccountUpdate ActualValue
		{
			get;
			set;
		}

		public decimal Difference
		{
			get
			{
				return ProjectedValue - ActualValue.UpdatedValue;
			}
		}
	}
}
