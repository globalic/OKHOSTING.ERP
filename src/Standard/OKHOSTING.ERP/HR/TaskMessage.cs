using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;

namespace OKHOSTING.ERP.HR
{
	[System.ComponentModel.DefaultProperty("Name")]
	public class TaskMessage : OKBaseObject
	{
		[RuleRequiredField]
		public Person Author
		{
			get { return GetPropertyValue<Person>("Author"); }
			set { SetPropertyValue("Author", value); }
		}

		[RuleRequiredField]
		public DateTime Date
		{
			get { return GetPropertyValue<DateTime>("Date"); }
			set { SetPropertyValue("Date", value); }
		}

		[Association]
		[RuleRequiredField]
		public Task Task
		{
			get { return GetPropertyValue<Task>("Task"); }
			set { SetPropertyValue("Task", value); }
		}

		[RuleRequiredField]
		[Size(SizeAttribute.Unlimited)]
		public String Content
		{
			get { return GetPropertyValue<String>("Content"); }
			set { SetPropertyValue("Content", value); }
		}

		public TaskMessage()
		{
		}

		public TaskMessage(DevExpress.Xpo.Session session): base(session)
		{
		}
	}
}