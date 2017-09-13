using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// En employee that will be assigned the tasks obtained from a task external source
	/// </summary>
	public class TaskSourceEmployee
	{
		public TaskSource Source
		{
			get;
			set;
		}

		public Employee Employee
		{
			get;
			set;
		}
	}
}