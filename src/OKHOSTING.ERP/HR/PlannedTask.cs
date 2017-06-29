using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.HR
{
	public class PlannedTask: Task
	{
		/// <summary>
		/// The proprity of the task
		/// </summary>
		public TaskPriority Priority { get; set; }

		public DateTime PlannedStart { get; set; }

		public TimeSpan PlannedTimeInvestment { get; set; }

		public DateTime PlannedFinish
		{
			get
			{
				return PlannedStart.Add(PlannedTimeInvestment);
			}
		}


	}
}