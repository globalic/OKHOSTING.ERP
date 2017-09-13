using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	public class PlannedActivity: Activity
	{
		[RequiredValidator]
		public TimeSpan EstimatedDuration
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime EstimatedStartDate
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime EstimatedEndDate
		{
			get;
			set;
		}
	}
}