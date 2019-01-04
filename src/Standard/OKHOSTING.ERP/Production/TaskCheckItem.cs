using System;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// Defines a task and subtasks that are reusable in similar projects
	/// </summary>
	public class TaskCheckItem : ORM.Model.Base<Guid>
	{
		[RequiredValidator]
		public string Title
		{
			get;
			set;
		}

		[RequiredValidator]
		public bool Completed
		{
			get;
			set;
		}

		[RequiredValidator]
		public Task Task
		{
			get;
			set;
		}
	}
}