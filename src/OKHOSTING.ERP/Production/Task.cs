﻿using System;
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

		int _Progress;

		/// <summary>
		/// Percentaje (from 0 to 100) of progress, how much is an activity finished
		/// </summary>
		[RequiredValidator]
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
				else if(_Progress >= 100)
				{
					_Progress = 99;
				}
			}
		}

		/// <summary>
		/// Task which this task belongs to
		/// </summary>
		public Task Parent
		{
			get;
			set;
		}

		/// <summary>
		/// Customer that is requesting this project. Use null for "internal" projects, no customer paying
		/// </summary>
		public Customers.Customer Customer
		{
			get;
			set;
		}

		[RequiredValidator]
		public TaskPriority Priority
		{
			get;
			set;
		} = TaskPriority.Normal;

		/// <summary>
		/// Date when the task started or is supposed to start
		/// </summary>
		public DateTime? StartDate
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

		/// <summary>
		/// Time invested in doing this task
		/// </summary>
		[RequiredValidator]
		public TimeSpan TimeInvestedTotal
		{
			get;
			set;
		}

		public DateTime? FinishDate
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

		public ICollection<Task> SubTasks
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

		/// <summary>
		/// Comments, files and directories related to this project
		/// </summary>
		public readonly ICollection<TaskAttachement> Updates;

		/// <summary>
		/// Warehouse transactions related to this project
		/// </summary>
		public readonly ICollection<Inventory.WarehouseTransaction> WarehouseTransactions;

		protected override void OnBeforeInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnBeforeInsert(sender, eventArgs);

			RecalculateValues();

			if (Parent != null)
			{
				if (Customer == null)
				{
					Customer  = Parent.Customer;
				}

				if (AssignedTo == null)
				{
					AssignedTo = Parent.AssignedTo;
				}
			}
		}

		protected override void OnBeforeUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnBeforeUpdate(sender, eventArgs);

			RecalculateValues();

			if (Parent != null)
			{
				if (Customer == null)
				{
					Customer = Parent.Customer;
				}

				if (AssignedTo == null)
				{
					AssignedTo = Parent.AssignedTo;
				}
			}
		}

		protected override void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterInsert(sender, eventArgs);

			if (Parent != null)
			{
				Parent.SelectOnce();
				Parent.RecalculateValues();
				Parent.Save();
			}
		}

		protected override void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterUpdate(sender, eventArgs);

			if (Parent != null)
			{
				Parent.SelectOnce();
				Parent.RecalculateValues();
				Parent.Save();
			}
		}

		protected void RecalculateValues()
		{
			TimeInvestedTotal = TimeInvested;

			if (SubTasks.Any())
			{
				foreach (Task sub in SubTasks)
				{
					sub.SelectOnce();
					sub.RecalculateValues();

					if (sub.StartDate < StartDate)
					{
						StartDate = sub.StartDate;
					}

					if (sub.FinishDate.Value > FinishDate)
					{
						FinishDate = sub.FinishDate;
					}
				}

				Progress = (int) SubTasks.Average(t => t.Progress);
				TimeInvestedTotal += TimeSpan.FromTicks(SubTasks.Sum(t => t.TimeInvestedTotal.Ticks));
			}

			if (Finished && FinishDate == null)
			{
				FinishDate = DateTime.Now;

				if (StartDate == null)
				{
					StartDate = FinishDate.Value.Subtract(TimeInvested);
				}
			}
			else if (!Finished)
			{
				FinishDate = null;
			}

			TotalSales = Invoices.Where(i => i is Customers.Sale).Sum(i => i.Total);
			TotalPurchases = Invoices.Where(i => i is Vendors.Purchase).Sum(i => i.Total);
			Balance = TotalSales - TotalPurchases;

			TotalSales += SubTasks.Sum(st => st.TotalSales);
			TotalPurchases += SubTasks.Sum(st => st.TotalPurchases);
			Balance += SubTasks.Sum(st => st.Balance);
		}
	}
}