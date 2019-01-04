using System;
using OKHOSTING.Data.Validation;


namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// En employee of the company
	/// </summary>
	public class EmployeeProspect: Person
	{
		[StringLengthValidator(50)]
		public string Role
		{
			get;
			set;
		}

		[RequiredValidator]
		public decimal MinSalary
		{
			get;
			set;
		}

		[RequiredValidator]
		public decimal MaxSalary
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
		public Department Department
		{
			get;
			set;
		}
	}
}