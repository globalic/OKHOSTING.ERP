using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// An external source for tasks, like an email account, a social network account, etc
	/// </summary>
	public abstract class TaskSource
	{
		/// <summary>
		/// List of employees that will be assigned the tasks that are obtained via this external source
		/// </summary>
		public ICollection<TaskSourceEmployee> Employees
		{
			get;
			set;
		}

		/// <summary>
		/// Retrieves the Tasks from the external source
		/// </summary>
		public abstract void Sync();

		/// <summary>
		/// Distributes the new generated tasks between all employees assigned to this source
		/// </summary>
		/// <param name="newTasks">List of newly crawled tasks</param>
		public void Distribute(List<Task> newTasks)
		{
			foreach (Task t in newTasks)
			{
				
			}
		}
	}
}