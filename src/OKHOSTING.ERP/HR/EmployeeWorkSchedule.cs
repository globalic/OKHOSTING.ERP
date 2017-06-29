using System;

namespace OKHOSTING.ERP.HR
{
	public class EmployeeWorkSchedule
	{
		public Employee Employee
		{
			get;
			set;
		}

		public DayOfWeek DayOfWeek
		{
			get;
			set;
		}

		public TimeSpan Start
		{
			get;
			set;
		}

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