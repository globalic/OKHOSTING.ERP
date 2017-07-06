using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	public class EmployeeWorkSession: ORM.PersistentClass<Guid>
	{
		[RequiredValidator]
		public Employee Employee { get; set; }

		[RequiredValidator]
		public DateTime StartedOn { get; set; }

		[RequiredValidator]
		public TimeSpan Lenght { get; set; }

		[RequiredValidator]
		public Location Location { get; set; }

		[RequiredValidator]
		public bool StartedOnTime { get; set; }

		[RequiredValidator]
		public bool FinishedOnTime { get; set; }
	}
}