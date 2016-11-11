using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace OKHOSTING.ERP.HR
{
	public class PlannedTask: Task
	{
		public PlannedTask()
		{
		}

		public PlannedTask(DevExpress.Xpo.Session session)
			: base(session)
		{
		}

		[Persistent]
		DateTime? PlannedStartDateParent { get; set; }

		[Persistent]
		public DateTime? PlannedEndDateParent { get; set; }

		//public int PlannedMinutesInvested
		//{
		//	get { return GetPropertyValue<int>("PlannedMinutesInvested"); }
		//	set { SetPropertyValue("PlannedMinutesInvested", value); }
		//}
	}
}