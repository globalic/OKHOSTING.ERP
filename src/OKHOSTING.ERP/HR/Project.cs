using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// The main class for tracking employee time
	/// </summary>
	public class Project: Task
	{
		#region General properties

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
		public Task Parent
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

		[RequiredValidator]
		public DateTime StartOn
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime EndOn
		{
			get;
			set;
		}

		#endregion

		#region Read only

		protected TimeSpan _TimeInvestedTotal;

		/// <summary>
		/// Time invested (in minutes) in doing this task so far, including all subtasks
		/// </summary>
		public TimeSpan TimeInvestedTotal
		{
			get { return _TimeInvestedTotal; }
		}

		[RequiredValidator]
		public override bool Finished
		{
			get
			{
				return (Progress >= 100);
			}
			set
			{
				if (value)
				{
					Progress = 100;
				}
				else if (Progress >= 100)
				{
					Progress = 0;
				}
			}
		}

		/// <summary>
		/// Duration of the task
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				if (StartOn == null)
				{
					return TimeSpan.Zero;
				}
				else if (EndOn == null)
				{
					return DateTime.Now - StartOn;
				}
				else
				{
					return (TimeSpan)(EndOn - StartOn);
				}
			}
		}

		/// <summary>
		/// The expenses incurred by the time dedicated to this task by the Employee
		/// </summary>
		public decimal TimeExpenses
		{
			get
			{
				if (AssignedTo == null)
				{
					return 0;
				}
				else
				{
					return (decimal)AssignedTo.Salary / 180 / 60 * TimeInvestedTotal;
				}
			}
		}

		#endregion

		#region Collections

		/// <summary>
		/// All tasks that 
		/// </summary>
		public ICollection<Task> SubTasks
		{
			get;
			set;
		}

		public ICollection<TaskAttachement> Attachements
		{
			get;
			set;
		}


		#endregion

		//public decimal TotalInvoiceItemsIncome
		//{
		//	get
		//	{
		//		return (decimal) EvaluateAlias("Finished");
		//	}
		//}

		//public decimal TotalOutcome
		//{
		//	get
		//	{
		//		return (decimal)EvaluateAlias("Finished");
		//	}
		//}
		
		#region Methods

		public void MarkAsFinished(int minutesInvested)
		{
			TimeInvested += minutesInvested;
			Progress = 100;

			//foreach (Task sub in SubTasks)
			//{
			//	sub.MarkAsFinished(minutesInvested);
			//}

			Save();
		}

		protected virtual void RecalculateValues()
		{
			_TimeInvestedTotal = TimeInvested;

			if (SubTasks.Count > 0)
			{
				foreach (Task s in SubTasks)
				{
					if (s.StartOn < StartOn)
					{
						StartOn = s.StartOn;
					}

					if (s.EndOn > EndOn)
					{
						EndOn = s.EndOn;
					}

					_TimeInvestedTotal += s.TimeInvestedTotal;
				}

				Progress = Convert.ToInt32(SubTasks.Average(t => t.Progress));
			}

			if (Finished && EndOn == null)
			{
				EndOn = DateTime.Now;
				
				if (StartOn == null)
				{
					StartOn = EndOn.AddMinutes(TimeInvested * -1);
				}
			}
			else if (!Finished)
			{
				//EndDate = null;
			}
		}

		/*
		[Action]
		public void Notify()
		{
			TaskMail mail = new TaskMail();
			mail.Task = this;
			mail.From = new System.Net.Mail.MailAddress(AssignedTo.Email, AssignedTo.FullName);
			mail.ReplyToList.Add(AssignedTo.Email);
			OKHOSTING.Tools.Net.Mail.MailManager.Send(mail);
		}
		*/		

		#endregion
	}
}