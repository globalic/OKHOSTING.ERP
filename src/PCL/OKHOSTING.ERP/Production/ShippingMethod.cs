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
	public class ShippingMethod: BaseObject
	{

		[RuleRequiredField]
		[Size(50)]
		public String Name
		{
			get { return GetPropertyValue<String>("Name"); }
			set { SetPropertyValue("Name", value); }
		}
		public ShippingMethod(): base(Session.DefaultSession)
		{
		}
		public ShippingMethod(Session session): base(session)
		{
		}
	}
}