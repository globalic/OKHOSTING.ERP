using System;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// Defines a task and subtasks that are reusable in similar projects
	/// </summary>
	public class TaskTemplate
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
		/// Complete description of this task
		/// </summary>
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Ammount of time estimated to complete the task
		/// </summary>
		public TimeSpan PlannedDuration
		{
			get;
			set;
		}

		/// <summary>
		/// Tasks are organized as a tree, so we can divide big tasks in smaller tasks
		/// </summary>
		public TaskTemplate Parent
		{
			get;
			set;
		}
	}
}