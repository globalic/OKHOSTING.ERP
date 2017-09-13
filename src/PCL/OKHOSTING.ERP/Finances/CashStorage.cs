using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace OKHOSTING.ERP.Finances
{
	[DefaultClassOptions]
	[System.ComponentModel.DefaultProperty("Name")]
	public class CashStorage: BaseObject
	{

		[Size(100)]
		public String Name
		{
			get { return GetPropertyValue<String>("Name"); }
			set { SetPropertyValue("Name", value); }
		}

		[Size(SizeAttribute.Unlimited)]
		public String Description
		{
			get { return GetPropertyValue<String>("Description"); }
			set { SetPropertyValue("Description", value); }
		}

		public Decimal Balance
		{
			get { return GetPropertyValue<Decimal>("Balance"); }
			set { SetPropertyValue("Balance", value); }
		}
		public CashStorage(): base(Session.DefaultSession)
		{
		}
		public CashStorage(Session session): base(session)
		{
		}
	}
}