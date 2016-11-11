using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Validation;
using System.ComponentModel;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// The main class for tracking employee time
	/// </summary>
	public class Task
	{
		#region General properties

		/// <summary>
		/// Name of the task, should summarize the hole task in a few words
		/// </summary>
		[StringLengthValidator(250)]
		[RequiredValidator]
		public string Subject
		{
			get;
			set;
		}

		[StringLengthValidator(100)]
		public string AuxId
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
		/// Employee who is responsible for doing this task
		/// </summary>
		public Employee AssignedTo
		{
            get;
            set;
		}

		/// <summary>
		/// Time invested (in minutes) in doing this task so far
		/// </summary>
		public int MinutesInvested
		{
            get;
            set;
		}

		public int PlannedMinutesInvested
		{
            get;
            set;
		}

		public TaskPriority Priority
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

		private ICollection<InvoiceItem> CompanyInvoiceItems
		{
            get;
            set;
		}

		/// <summary>
		/// InvoiceItem that is related to this activity in the way it involved a sale or a purchase.
		/// </summary>
		/// <remarks>
		/// Used to link activities with sales and purchases, and keep tracking of how much money is spent
		/// or earned realted to the ammount of time invested in activities to complete the sale or purchase
		/// </remarks>
		public InvoiceItem InvoiceItem
		{
            get;
            set;
		}

		/// <summary>
		/// Percentaje (from 0 to 100) of progress, how much is an activity finished
		/// </summary>
		[RequiredValidator]
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

		//[Persistent("MinutesInvestedTotal")]
		protected decimal _MinutesInvestedTotal;

		/// <summary>
		/// Time invested (in minutes) in doing this task so far, including all subtasks
		/// </summary>
		public decimal MinutesInvestedTotal
		{
			get { return _MinutesInvestedTotal; }
		}

		/// <summary>
		/// Time invested (in hours) in doing this task so far, including all subtasks
		/// </summary>
		public decimal HoursInvestedTotal
		{
			get
			{
				return MinutesInvestedTotal / 60;
			}
		}

		public bool Finished
		{
			get
			{
				return (bool)(Progress >= 100);
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
					return (decimal)AssignedTo.Salary / 180 / 60 * MinutesInvestedTotal;
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

		public ICollection<TaskCollaborator> Collaborators
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

		/*
		[Action]
		public void MarkAsFinished()
		{
			MarkAsFinished(0);
		}
		*/

		public void MarkAsFinished(int minutesInvested)
		{
			MinutesInvested += minutesInvested;
			Progress = 100;

			//foreach (Task sub in SubTasks)
			//{
			//	sub.MarkAsFinished(minutesInvested);
			//}

			Save();
		}

		protected void AutoLabel()
		{
			//label
			if (Finished)
			{
				Label = 0;
			}
			else
			{
				Label = 5;
			}
		}

		protected void RecalculateValues()
		{
			_MinutesInvestedTotal = MinutesInvested;

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

					_MinutesInvestedTotal += s.MinutesInvestedTotal;
				}

				Progress = Convert.ToInt32(Evaluate("SubTasks.Avg(Progress)"));
			}

			SetPropertyValue<decimal>("MinutesInvestedTotal", _MinutesInvestedTotal);

			if (Finished && EndOn == null)
			{
				EndOn = DateTime.Now;
				
				if (StartOn == null)
				{
					StartOn = EndOn.AddMinutes(MinutesInvested * -1);
				}
			}
			else if (!Finished)
			{
				//EndDate = null;
			}

			//if this task has collaborators, clone the curent task for each coollaborator who participated on it
			foreach (TaskCollaborator collaborator in Collaborators)
			{
				//is there already a clone task for this collaborator?
				Task collaborationTask = Session.FindObject<Task>(CriteriaOperator.And(new BinaryOperator("Parent", this), new BinaryOperator("AssignedTo", collaborator.Collaborator), new BinaryOperator("Subject", "Colaboración")));

				//if does not exist, create it
				if (collaborationTask == null)
				{
					collaborationTask = new Task(Session);
				}

				collaborationTask.Subject = "Colaboración";
				collaborationTask.AssignedTo = collaborator.Collaborator;
				collaborationTask.StartOn = StartOn;
				collaborationTask.EndOn = EndOn;
				collaborationTask.MinutesInvested = MinutesInvested;
				collaborationTask.Parent = this;
				collaborationTask.Progress = this.Progress;
				collaborationTask.Save();
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
		
		#region ITreeNode

		#endregion

		#region Constructors

		#endregion

		#region IEvent / ISupportRecurrences

		#region NonPersistent

		public bool AllDay
		{
			get;
			set;

		}

		public object AppointmentId
		{
			get 
			{ 
				return Oid; 
			}
		}

		public string Location
		{
			get;
			set;

		}

		public string ResourceId
		{
			get;
			set;
		}
		
		#endregion

		public int Type
		{
			get;
			set;
		}

		public int Label
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string RecurrenceInfoXml
		{
			get;
			set;
		}

		#endregion

		public class Configuration : OKHOSTING.Tools.ConfigurationBase
		{
			public int LastAuxId = 0;

			public static Configuration Current;

			/// <summary>
			/// Loads the current configuration
			/// </summary>
			static Configuration()
			{
				Current = OKHOSTING.Tools.ConfigurationBase.Current<Configuration>();
			}
		}
	}
}