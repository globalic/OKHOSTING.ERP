using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// The main class for tracking employee time
	/// </summary>
	public class Project: Task
	{
		public Project()
		{
			Attachements = new Data.CachedCollection<ProjectAttachement>(new ORM.Model.ForeignKeyFilteredCollection<Project, ProjectAttachement>(this, t => t.Project));
			Invoices = new Data.CachedCollection<Invoice>(new ORM.Model.ForeignKeyFilteredCollection<Project, Invoice>(this, i => i.Project));
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
		/// Percentaje (from 0 to 100) of progress, how much is an activity finished
		/// </summary>
		[RequiredValidator]
		[RangeValidator(0, 100)]
		public int Progress
		{
			get;
			set;
		}

		/// <summary>
		/// All tasks that 
		/// </summary>
		public ICollection<Task> Tasks
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
		/// Files and directories related to this project
		/// </summary>
		public readonly ICollection<ProjectAttachement> Attachements;

		/// <summary>
		/// Warehouse transactions related to this project
		/// </summary>
		public readonly ICollection<Inventory.WarehouseTransaction> WarehouseTransactions;
	}
}