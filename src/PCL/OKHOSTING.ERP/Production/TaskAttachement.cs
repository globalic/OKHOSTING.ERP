using System;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Production
{
	/// <summary>
	/// An attachement to a task, including an optional file and an also optional comment
	/// </summary>
	public class TaskAttachement
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		public Task Task
		{
			get;
			set;
		}

		[RequiredValidator]
		public HR.Employee RegisteredBy
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime Date
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Comment
		{
			get;
			set;
		}

		//public Files.Element Attachement
		//{
		//	get;
		//	set;
		//}
	}
}