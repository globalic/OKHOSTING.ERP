using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// En employee of the company
	/// </summary>
	public class Employee : Person
	{
		[StringLengthValidator(50)]
		public string Role
		{
			get;
			set;
		}

		[RequiredValidator]
		public decimal Salary
		{
			get;
			set;
		}

		[RequiredValidator]
		public SalaryType SalaryType
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime EnrollmentDate
		{
			get;
			set;
		}

		public Team Team
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates if the employee is still working for the company or not
		/// </summary>
		[RequiredValidator]
		public bool Active
		{
			get;
			set;
		}

		public byte[] Picture
		{
			get;
			set;
		}

		//	public ICollection<Task> AssignedTasks
		//	{
		//		   get;
		//		   set;
		//	}

		//	public ICollection<Task> OpenTasks
		//	{
		//		   get;
		//		   set;
		//	}
	}
}