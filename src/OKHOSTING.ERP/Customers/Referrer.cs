using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace OKHOSTING.Softosis.ERP.Customers
{
	[DefaultClassOptions]
	[System.ComponentModel.DefaultProperty("Name")]
	public class Referrer: Customer
	{

		[RuleRequiredField]
		public Decimal Comission
		{
			get { return GetPropertyValue<Decimal>("Comission"); }
			set { SetPropertyValue("Comission", value); }
		}
		public Referrer(): base(Session.DefaultSession)
		{
		}
		public Referrer(Session session): base(session)
		{
		}
	}
}