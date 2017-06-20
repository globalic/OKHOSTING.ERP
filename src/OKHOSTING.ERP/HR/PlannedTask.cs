using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.HR
{
	public class PlannedTask: Task
	{
		DateTime? PlannedStartDateParent { get; set; }

		public DateTime? PlannedEndDateParent { get; set; }

		//public int PlannedMinutesInvested
		//{
		//	get { return GetPropertyValue<int>("PlannedMinutesInvested"); }
		//	set { SetPropertyValue("PlannedMinutesInvested", value); }
		//}
	}
}