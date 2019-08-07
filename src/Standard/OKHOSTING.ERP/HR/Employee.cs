using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using System.Linq;

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

		/// <summary>
		/// List of thasts directly assigned to this employee
		/// </summary>
		public ICollection<Production.Task> AssignedTasks
		{
			get;
			set;
		}

		/// <summary>
		/// List of credentials assigned to this employee
		/// </summary>
		public ICollection<IT.EmployeeCredentials> Credentials
		{
			get;
			set;
		}
		 
		/// <summary>
		/// Returns the list of task that are assigned to this employee and not finished
		/// </summary>
		public IEnumerable<Production.Task> OpenTasks
		{
			get
			{
				return AssignedTasks?.Where(t => !t.Finished);
			}
		}

		public ICollection<EmployeeWorkSchedule> WorkSchedules
		{
			get;
			set;
		}

		public ICollection<EmployeeWorkSession> WorkSessions
		{
			get;
			set;
		}

		public decimal SalaryPerHour
		{
			get
			{
				int daysPerSalary = 0;

				switch (SalaryType)
				{
					case SalaryType.PerBiWeek:
						daysPerSalary = 14;
						break;

					case SalaryType.PerDay:
						daysPerSalary = 1;
						break;

					case SalaryType.PerMonth:
						daysPerSalary = 28;
						break;

					case SalaryType.PerWeek:
						daysPerSalary = 7;
						break;

					case SalaryType.PerWork:
					case SalaryType.PerHour:
					default:
						return 0;
				}

				decimal scheduledHoursPerWeek = (decimal) TimeSpan.FromTicks(WorkSchedules.Sum(s => s.Lenght.Ticks)).TotalHours;
				decimal averageHoursPerDay = scheduledHoursPerWeek / 7;
				decimal hoursPerSalary = daysPerSalary * averageHoursPerDay;

				return Salary / hoursPerSalary;
			}
		}
	}
}