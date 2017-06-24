using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	public class EmployeeWorkSession
	{
		public Guid Id { get; set; }
		public Employee Employee { get; set; }
		public DateTime StartedOn { get; set; }
		public TimeSpan Lenght { get; set; }

		public bool StartedOnTime
		{
			get;
			set;
		}

		public bool FinishedOnTime
		{
			get;
			set;
		}
	}
}