using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.ERP.HR;

namespace OKHOSTING.ERP.HR
{
	public class ActivityUpdate
	{
		public Activity Activity
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime StartDate
		{
			get;
			set;
		}

		/// <summary>
		/// Time invested (in hours) in performing the activity or part of the activity
		/// </summary>
		[RequiredValidator]
		public decimal HoursInvested
		{
			get;
			set;
		}

		public DateTime EndDate
		{
			get 
			{
				return StartDate.AddHours((double) HoursInvested);
			}
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Notes
		{
			get;
			set;
		}

		[RequiredValidator]
		public Employee Employee
		{
			get;
			set;
		}
	}
}