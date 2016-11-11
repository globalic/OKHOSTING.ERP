using System;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	public class Team
	{
		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		public Team Parent
		{
			get;
			set;
		}

		public ICollection<Employee> Employees
		{
			get;
			set;
		}

	}
}