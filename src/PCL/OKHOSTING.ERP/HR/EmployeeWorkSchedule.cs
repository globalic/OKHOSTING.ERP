using OKHOSTING.Data.Validation;
using System;

namespace OKHOSTING.ERP.HR
{
	public class EmployeeWorkSchedule
	{
		[RequiredValidator]
		public Employee Employee
		{
			get;
			set;
		}

		[RequiredValidator]
		public DayOfWeek DayOfWeek
		{
			get;
			set;
		}

		[RequiredValidator]
		public TimeSpan Start
		{
			get;
			set;
		}

		[RequiredValidator]
		public TimeSpan Lenght
		{
			get;
			set;
		}

		public TimeSpan End
		{
			get 
			{
				var end = Start + Lenght;

				if (end > TimeSpan.FromHours(24))
				{
					return end - TimeSpan.FromHours(24);
				}
				else
				{
					return end;
				}
			}
		}
	}
}