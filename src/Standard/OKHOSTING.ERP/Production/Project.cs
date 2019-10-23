using System;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A group of tasks that are organized in chronological order
	/// </summary>
	public class Project
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Name of this project
		/// </summary>
		[StringLengthValidator(250)]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Name of this project
		/// </summary>
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		public HR.Team Team
		{
			get;
			set;
		}

		public Project Parent
		{
			get;
			set;
		}

		public ICollection<Project> Subprojects
		{
			get;
			set;
		}

		public ICollection<Task> Tasks
		{
			get;
			set;
		}

		public ICollection<Invoice> Invoices
		{
			get;
			set;
		}
	}
}