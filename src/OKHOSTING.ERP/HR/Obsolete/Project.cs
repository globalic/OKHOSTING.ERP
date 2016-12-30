using System;
using OKHOSTING.Data.Validation;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace OKHOSTING.ERP.HR
{
	public class Project
	{
		[RequiredValidator]
		public bool Finished
		{
			get
			{
				return Progress == 100;
			}
		}

		[RequiredValidator]
		public TimeSpan EstimatedDuration
		{
			get;
			set;
		}

		/// <summary>
		/// Duration of the activity
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				if (Start == null)
				{
					return TimeSpan.Zero;
				}
				else if (End == null)
				{
					return DateTime.Now - Start.Value;
				}
				else
				{
					return (TimeSpan) (End - Start);
				}
			}
		}

		public Department Department
		{
			get;
			set;
		}

		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}

		[RequiredValidator]
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Objectives
		{
			get;
			set;
		}

		[RequiredValidator]
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Strategy
		{
			get;
			set;
		}

		/// <summary>
		/// Percentaje (from 0 to 100) of progress, how much is an activity finished
		/// </summary>
		//[Custom("DisplayFormat", "{0%}")]
		public ushort Progress
		{
			get
			{
				ushort sum = 0;
				int count = 0;

				foreach (Project p in SubProjects)
				{
					sum += p.Progress;
					count++;
				}
				
				foreach (Activity a in Activities)
				{
					sum += a.Progress;
					count++;
				}
				
				return (ushort)((count == 0) ? 0 : sum / count);
			}
		}

		/// <summary>
		/// Time invested (in hours) in performing the activity or part of the activity
		/// </summary>
		public decimal HoursInvested
		{
			get;
			set;
		}

		[RequiredValidator]
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Evaluation
		{
			get;
			set;
		}

		[RequiredValidator]
		public bool ObjectivesAccomplished
		{
			get;
			set;
		}

		[RequiredValidator]
		public bool OnBudget
		{
			get;
			set;
		}

		[RequiredValidator]
		public bool OnSchedule
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime EstimatedStartDate
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime EstimatedEndDate
		{
			get;
			set;
		}

		private DateTime? _Start = null;

		/// <summary>
		/// Starting date for the project, calculated automatically using the first project's task starting date
		/// </summary>
		public DateTime? Start
		{
			get;
			set;
		}
		
		private DateTime? _End = null;

		/// <summary>
		/// Ending date for the project, calculated automatically using the last project's task ending date
		/// </summary>
		public DateTime? End
		{
			get;
			set;
		}

		public Project Parent
		{
			get;
			set;
		}

		public ICollection<Project> SubProjects
		{
			get;
			set;
		}

		public ICollection<Activity> Activities
		{
			get;
			set;
		}

	

		//private void CalculateStart(bool forceChangeEvents)
		//{
		//	DateTime? _Start_old = _Start;
		//	_Start = null;

		//	foreach (Activity a in Activities)
		//	{
		//		if (_Start == null || a.StartDate < _Start) _Start = a.StartDate;
		//	}

		//	if (forceChangeEvents)
		//	{
		//		OnChanged("Start", _Start_old, _Start);
		//	}
		//}

		//private void CalculateEnd(bool forceChangeEvents)
		//{
		//	DateTime? _End_old = _End;
		//	_End = null;

		//	foreach (Activity a in Activities)
		//	{
		//		if (_End == null || a.EndDate > _End) _End = a.EndDate;
		//	}

		//	if (forceChangeEvents)
		//	{
		//		OnChanged("End", _End_old, _End);
		//	}
		//}
	}
}