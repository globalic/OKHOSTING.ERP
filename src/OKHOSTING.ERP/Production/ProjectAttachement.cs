using System;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// Defines a task and subtasks that are reusable in similar projects
	/// </summary>
	public class ProjectAttachement: ORM.Model.Base<Guid>
	{
		[RequiredValidator]
		public Files.Element Attachement
		{
			get;
			set;
		}


		public Project Project
		{
			get;
			set;
		}
	}
}