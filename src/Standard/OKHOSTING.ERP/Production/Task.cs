using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.Production
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
		[StringLengthValidator(500)]
		public string Name
		{
			get;
			set;
		}

		bool _Finished;

		/// <summary>
		/// Indicates if the task has been finished or not
		/// </summary>
		[RequiredValidator]
		public bool Finished
		{
			get
			{
				return _Finished;
			}
			set
			{
				_Finished = value;

				if (_Finished)
				{
					_Progress = 100;
				}
				else if (_Progress >= 100)
				{
					_Progress = 0;
				}
			}
		}

		/// <summary>
		/// Date when the task started or is supposed to start
		/// </summary>
		public DateTime? StartDate
		{
			get;
			set;
		}

		/// <summary>
		/// Date when the task was finished or is supposed to finish in the furture
		/// </summary>
		public DateTime? EndDate
		{
			get;
			set;
		}

		/// <summary>
		/// Time invested in doing this task, or estimated time to invest
		/// </summary>
		public TimeSpan TimeInvested
		{
			get;
			set;
		}

		/// <summary>
		/// Employee who is responsible for doing this task
		/// </summary>
		public HR.Employee Owner
		{
			get;
			set;
		}

		int _Progress;

		/// <summary>
		/// Percentaje (from 0 to 100) of progress, how much is an activity finished
		/// </summary>
		[RangeValidator(0, 100)]
		public int Progress
		{
			get
			{
				return _Progress;
			}
			set
			{
				_Progress = value;
				_Finished = value >= 100;
			}
		}

		[RequiredValidator]
		public TaskPriority Priority
		{
			get;
			set;
		} = TaskPriority.Normal;

		/// <summary>
		/// Customer that is requesting this project. Use null for "internal" projects, no customer paying
		/// </summary>
		public Customers.Customer Customer
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
		/// Comments, files and directories related to this project
		/// </summary>
		public readonly ICollection<TaskAttachement> Attachements;

		/// <summary>
		/// Schedules for this task
		/// </summary>
		public readonly ICollection<TaskSchedule> Schedules;

		public Task Clone()
		{
			return (Task) MemberwiseClone();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}