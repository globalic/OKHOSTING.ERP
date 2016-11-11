using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A person who, besides the Task owner (AssignedTo), helps with the current task, in a team work.
	/// So when a collaborator is captured in a task, a subtask is generatod for that collaborator
	/// with the exact dates and investedminutes as the parent task. Usefull to register meetings or other
	/// types of team-performed tasks
	/// </summary>
	public class TaskCollaborator
	{
		public Task Task
		{
			get;
			set;
		}

		public Employee Collaborator
		{
			get;
			set;
		}

	}
}