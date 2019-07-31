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
		/// Cost of time invested, considering the employee's salary
		/// </summary>
		public decimal TimeInvestedCost
		{
			get;
			set;
		}

		/// <summary>
		/// Cost of time invested, considering the employee's salary, in total
		/// </summary>
		public decimal TimeInvestedCostTotal
		{
			get;
			set;
		}

		/// <summary>
		/// Employee who is responsible for doing this task
		/// </summary>
		[RequiredValidator]
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

		public decimal SoldTotal
		{
			get;
			set;
		}

		public decimal PurchasedTotal
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
		public ICollection<InvoiceItem> InvoiceItems
		{
			get;
			set;
		}

		public IEnumerable<Product> SoldProducts
		{
			get
			{
				return InvoiceItems?.Where(i => i.Invoice.InvoiceType == InvoiceType.Sale).Select(i => i.Product);
			}
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

		/// <summary>
		/// Tasks that conform this task as a group. This is just a way of grouping tasks.
		/// You could think of subtasks as prerequisites of the parent task. Once all subtasks as completed 
		/// (and only then),the parent task is also considered completed.
		/// </summary>
		public ICollection<Task> SubTasks
		{
			get;
			set;
		}

		/// <summary>
		/// Comments, files and directories related to this project
		/// </summary>
		//public readonly ICollection<TaskAttachement> Attachements;

		/// <summary>
		/// Schedules for this task
		/// </summary>
		public readonly ICollection<TaskSchedule> Schedules;

		/// <summary>
		/// Warehouse transactions related to this project
		/// </summary>
		//public readonly ICollection<Inventory.WarehouseTransaction> WarehouseTransactions;

		/// <summary>
		/// Recalculates Progress, StartDate, EndDate, TimeInvestedTotal, TotalSales, TotalPurchases and Balance properties based on SubTasks and Invoices
		/// </summary>
		public void RecalculateValues()
		{
			TimeInvestedTotal = TimeInvested;
			SoldTotal = PurchasedTotal = TimeInvestedCost = 0;
			TimeInvestedCost = (decimal) TimeInvested.TotalHours * Owner.SalaryPerHour;

			//foreach (var s in SubTasks)
			//{
			//	s.RecalculateValues();
			//}

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

				Progress = (int) SubTasks.Average(t => t.Progress);
				TimeInvestedTotal += TimeSpan.FromTicks(SubTasks.Sum(t => t.TimeInvestedTotal.Ticks) + TimeInvested.Ticks);
				TimeInvestedCostTotal = SubTasks.Sum(st => st.TimeInvestedCostTotal) + TimeInvestedCost;
				SoldTotal += SubTasks.Sum(st => st.SoldTotal);
				PurchasedTotal += SubTasks.Sum(st => st.PurchasedTotal);
			}

			if (InvoiceItems.Any())
			{
				SoldTotal += InvoiceItems.Where(i => i.Invoice.InvoiceType == InvoiceType.Sale).Sum(i => i.Total);
				PurchasedTotal += InvoiceItems.Where(i => i.Invoice.InvoiceType == InvoiceType.Purchase).Sum(i => i.Total);
			}

			Balance = SoldTotal - PurchasedTotal - TimeInvestedCostTotal;
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

		//public void OnBeforeInsert(DataBase sender, OperationEventArgs eventArgs)
		//{
		//	//base.OnBeforeInsert(sender, eventArgs);

		//	if (Parent != null)
		//	{
		//		if (Customer == null)
		//		{
		//			Customer = Parent.Customer;
		//		}

		//		if (AssignedTo == null)
		//		{
		//			AssignedTo = Parent.AssignedTo;
		//		}
		//	}
		//}

		//public void OnBeforeUpdate(DataBase sender, OperationEventArgs eventArgs)
		//{
		//	//base.OnBeforeUpdate(sender, eventArgs);

		//	if (Parent != null)
		//	{
		//		if (Customer == null)
		//		{
		//			Customer = Parent.Customer;
		//		}

		//		if (AssignedTo == null)
		//		{
		//			AssignedTo = Parent.AssignedTo;
		//		}
		//	}
		//}

		//public void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		//{
		//	//base.OnAfterInsert(sender, eventArgs);

		//	if (Parent != null)
		//	{
		//		sender.Select(Parent);
		//		Parent.RecalculateValues();
		//		sender.Save(Parent);
		//	}
		//}

		//public void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		//{
		//	//base.OnAfterUpdate(sender, eventArgs);

		//	if (Parent != null)
		//	{
		//		sender.Select(Parent);
		//		Parent.RecalculateValues();
		//		sender.Save(Parent);
		//	}
		//}
	}
}