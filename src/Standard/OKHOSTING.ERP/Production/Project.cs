using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A group of tasks that are organized in chronological order
	/// </summary>
	public class Project: ORM.Model.Base<Guid>
	{
		/// <summary>
		/// Customer that is requesting this project. Use null for "internal" projects, no customer paying
		/// </summary>
		public Customers.Customer Customer
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
		/// Name of this project
		/// </summary>
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Percentaje (from 0 to 100) of progress, how much is an activity finished
		/// </summary>
		[RequiredValidator]
		[RangeValidator(0, 100)]
		public int Progress
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