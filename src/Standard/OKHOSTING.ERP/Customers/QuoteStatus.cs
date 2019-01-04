using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;

namespace OKHOSTING.ERP.Customers
{
	[DefaultClassOptions]
	[System.ComponentModel.DefaultProperty("Name")]
	public class QuoteStatus : BaseObject
	{
		[RuleRequiredField]
		[Size(100)]
		public String Name
		{
			get { return GetPropertyValue<String>("Name"); }
			set { SetPropertyValue("Name", value); }
		}

		public QuoteStatus(): base(Session.DefaultSession)
		{
		}
		public QuoteStatus(Session session): base(session)
		{
		}
	}
}