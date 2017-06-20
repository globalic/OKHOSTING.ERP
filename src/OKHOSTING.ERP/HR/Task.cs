using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// The main class for tracking employee time
	/// </summary>
	public class Task
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Name of the task, should summarize the hole task in a few words
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Employee who is responsible for doing this task
		/// </summary>
		public Employee AssignedTo
		{
			get;
			set;
		}

		/// <summary>
		/// The proprity of the task
		/// </summary>
		public TaskPriority Priority
		{
			get;
			set;
		}

		/// <summary>
		/// Tasks are organized as a tree, so we can divide big tasks in smaller tasks
		/// </summary>
		public Project Parent
		{
			get;
			set;
		}

		/// <summary>
		/// The company (customer or vendor) which this activity is related to, if any.
		/// </summary>
		public Company Company
		{
			get;
			set;
		}

		public DateTime StartedOn
		{
			get;
			set;
		}

		/// <summary>
		/// Time invested (in minutes) in doing this task so far
		/// </summary>
		public TimeSpan TimeInvested
		{
			get;
			set;
		}

		public DateTime FinishedOn
		{
			get
			{
				return StartedOn.Add(TimeInvested);
			}
		}

		[RequiredValidator]
		public virtual bool Finished
		{
			get;
			set;
		}
	}
}