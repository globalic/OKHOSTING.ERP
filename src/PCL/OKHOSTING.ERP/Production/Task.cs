using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Production
{
	/// <summary>
	/// The main class for tracking employee time
	/// </summary>
	public class Task
	{
		#region Basic properties

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

		#endregion

		#region Management

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
		[RequiredValidator]
		public HR.Employee AssignedTo
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

		#endregion

		#region Finance

		/// <summary>
		/// Customer that is requesting this project. Use null for "internal" projects, no customer paying
		/// </summary>
		public Customers.Customer Customer
		{
			get;
			set;
		}

		public decimal TotalSales
		{
			get;
			set;
		}

		public decimal TotalPurchases
		{
			get;
			set;
		}

		public decimal Balance
		{
			get;
			set;
		}

		/// <summary>
		/// invoices related to this project
		/// </summary>
		public ICollection<Invoice> Invoices
		{
			get;
			set;
		}

		#endregion

		#region Subtasks, updates and inventory

		/// <summary>
		/// Task which this task belongs to
		/// </summary>
		public Task Parent
		{
			get;
			set;
		}

		/// <summary>
		/// Time invested in doing this task
		/// </summary>
		[RequiredValidator]
		public TimeSpan TimeInvestedTotal
		{
			get;
			set;
		}

		public ICollection<Task> SubTasks
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

		/// <summary>
		/// Warehouse transactions related to this project
		/// </summary>
		public readonly ICollection<Inventory.WarehouseTransaction> WarehouseTransactions;

		/// <summary>
		/// Recalculates Progress, StartDate, EndDate, TimeInvestedTotal, TotalSales, TotalPurchases and Balance properties based on SubTasks and Invoices
		/// </summary>
		public void RecalculateValues()
		{
			TimeInvestedTotal = TimeInvested;
			TotalSales = TotalPurchases = 0;

			if (SubTasks.Any())
			{
				foreach (Task sub in SubTasks)
				{
					if (sub.StartDate < StartDate)
					{
						StartDate = sub.StartDate;
					}

					if (sub.EndDate.Value > EndDate)
					{
						EndDate = sub.EndDate;
					}
				}

				Progress = (int)SubTasks.Average(t => t.Progress);
				TimeInvestedTotal += TimeSpan.FromTicks(SubTasks.Sum(t => t.TimeInvestedTotal.Ticks));

				if (Finished && EndDate == null)
				{
					EndDate = DateTime.Now;

					if (StartDate == null)
					{
						StartDate = EndDate.Value.Subtract(TimeInvested);
					}
				}
				else if (!Finished)
				{
					EndDate = null;
				}

				TotalSales += SubTasks.Sum(st => st.TotalSales);
				TotalPurchases += SubTasks.Sum(st => st.TotalPurchases);
				Balance += SubTasks.Sum(st => st.Balance);
			}

			if (Invoices.Any())
			{
				TotalSales = Invoices.Where(i => i is Customers.Sale).Sum(i => i.Total);
				TotalPurchases = Invoices.Where(i => i is Vendors.Purchase).Sum(i => i.Total);
			}

			Balance = TotalSales - TotalPurchases;
		}

		#endregion


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