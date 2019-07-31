using OKHOSTING.Data.Validation;
using System;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A way of grouping task in meaningfull groups
	/// </summary>
	/// <remarks>
	/// A recommended category tree would include all company departments at the top, then, subdepartments, 
	/// and then the most usual task performed in each subdepartment
	/// </remarks>
	public class TaskCategory
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}

		public TaskCategory Parent
		{
			get;
			set;
		}

		public ICollection<TaskCategory> Subcategories
		{
			get;
			set;
		}
	}
}