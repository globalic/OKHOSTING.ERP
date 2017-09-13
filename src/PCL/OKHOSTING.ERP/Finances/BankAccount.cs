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
	public class BankAccount: BaseObject
	{

		[Size(SizeAttribute.Unlimited)]
		public String BankName
		{
			get { return GetPropertyValue<String>("BankName"); }
			set { SetPropertyValue("BankName", value); }
		}

		[Size(SizeAttribute.Unlimited)]
		public String AccountNumber
		{
			get { return GetPropertyValue<String>("AccountNumber"); }
			set { SetPropertyValue("AccountNumber", value); }
		}

		[Size(SizeAttribute.Unlimited)]
		public String AlternativeNumber
		{
			get { return GetPropertyValue<String>("AlternativeNumber"); }
			set { SetPropertyValue("AlternativeNumber", value); }
		}

		[Size(SizeAttribute.Unlimited)]
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
		public BankAccount(): base(Session.DefaultSession)
		{
		}
		public BankAccount(Session session): base(session)
		{
		}
	}
}