using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace OKHOSTING.ERP.Production
{
	[DefaultClassOptions]
	[System.ComponentModel.DefaultProperty("Name")]
	public class Store: BaseObject
	{

		[RuleRequiredField]
		[Size(100)]
		public String Name
		{
			get { return GetPropertyValue<String>("Name"); }
			set { SetPropertyValue("Name", value); }
		}

		[RuleRequiredField]
		[Size(100)]
		public String Description
		{
			get { return GetPropertyValue<String>("Description"); }
			set { SetPropertyValue("Description", value); }
		}

		[Size(200)]
		public String Url
		{
			get { return GetPropertyValue<String>("Url"); }
			set { SetPropertyValue("Url", value); }
		}
		public Store(): base(Session.DefaultSession)
		{
		}
		public Store(Session session): base(session)
		{
		}
	}
}