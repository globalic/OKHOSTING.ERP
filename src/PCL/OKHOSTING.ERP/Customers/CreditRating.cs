using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace OKHOSTING.ERP.Customers
{
	[DefaultClassOptions]
	[System.ComponentModel.DefaultProperty("Name")]
	public class CreditRating: BaseObject
	{

		[RuleRequiredField]
		[Size(50)]
		public String Name
		{
			get { return GetPropertyValue<String>("Name"); }
			set { SetPropertyValue("Name", value); }
		}
		public CreditRating(): base(Session.DefaultSession)
		{
		}
		public CreditRating(Session session): base(session)
		{
		}
	}
}