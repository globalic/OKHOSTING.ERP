using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A phase in a project, defined in a specific period in time and goals
	/// </summary>
	public class ProjectPhase : ORM.Model.Base<Guid>
	{
		/// <summary>
		/// Customer that is requesting this project. Use null for "internal" projects, no customer paying
		/// </summary>
		public Project Project
		{
			get;
			set;
		}

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
		/// Date when the task started or is supposed to start
		/// </summary>
		public DateTime StartDate
		{
			get;
			set;
		} = DateTime.Now;

		/// <summary>
		/// Time invested in doing this task
		/// </summary>
		public TimeSpan TimeInvested
		{
			get;
			set;
		}

		public DateTime? FinishDate
		{
			get;
			set;
		}

		/// <summary>
		/// invoices related to this project
		/// </summary>
		public ICollection<Task> Tasks
		{
			get;
			set;
		}
	}
}