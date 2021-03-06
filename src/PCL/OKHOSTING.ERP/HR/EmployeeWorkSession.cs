﻿using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.HR
{
	public class EmployeeWorkSession
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		public Employee Employee { get; set; }

		[RequiredValidator]
		public DateTime StartedOn { get; set; }

		[RequiredValidator]
		public TimeSpan Lenght { get; set; }

		[RequiredValidator]
		public Address Location { get; set; }

		[RequiredValidator]
		public bool StartedOnTime { get; set; }

		[RequiredValidator]
		public bool FinishedOnTime { get; set; }
	}
}