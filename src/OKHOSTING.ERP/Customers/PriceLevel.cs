using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace OKHOSTING.ERP.Customers
{
	/// <summary>
	/// Defines a percentage that can be increased or decreased to all sales
	/// made to customers that belong to one price level
	/// </summary>
	[System.ComponentModel.DefaultProperty("Name")]
	public class PriceLevel: BaseObject
	{
		[RuleRequiredField]
		[Size(100)]
		public String Name
		{
			get { return GetPropertyValue<String>("Name"); }
			set { SetPropertyValue("Name", value); }
		}

		[RuleRequiredField]
		public Decimal PriceVariation
		{
			get { return GetPropertyValue<Decimal>("PriceVariation"); }
			set { SetPropertyValue("PriceVariation", value); }
		}

		public PriceLevel(): base(Session.DefaultSession)
		{
		}
		public PriceLevel(Session session): base(session)
		{
		}
	}
}