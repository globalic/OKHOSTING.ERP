using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// The main class for tracking employee time
	/// </summary>
	public class Task: ORM.Model.Base<Guid>
	{
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
		[RequiredValidator]
		public HR.Employee AssignedTo
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates if the task has been finished or not
		/// </summary>
		[RequiredValidator]
		public virtual bool Finished
		{
			get;
			set;
		}

		/// <summary>
		/// Project which this task belongs to
		/// </summary>
		public Project Project
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
		}

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
			get
			{
				if (Finished)
				{
					return StartDate.Add(TimeInvested);
				}
				else
				{
					return null;
				}
			}
		}
	}
}