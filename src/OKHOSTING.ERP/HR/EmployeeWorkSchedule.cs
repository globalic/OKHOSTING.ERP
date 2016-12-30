using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public TimeSpan End
		{
			get;
			set;
		}

		public TimeSpan Lenght
		{
			get 
			{
				return End - Start; 
			}
		}
	}
}