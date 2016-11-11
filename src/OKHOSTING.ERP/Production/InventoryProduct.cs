using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace OKHOSTING.Softosis.ERP.Production
{
	[DefaultClassOptions]
	[System.ComponentModel.DefaultProperty("Name")]
	public class InventoryProduct: Product
	{

		[RuleRequiredField]
		public Decimal OnHand
		{
			get { return GetPropertyValue<Decimal>("OnHand"); }
			set { SetPropertyValue("OnHand", value); }
		}

		[RuleRequiredField]
		public Decimal ReorderPoint
		{
			get { return GetPropertyValue<Decimal>("ReorderPoint"); }
			set { SetPropertyValue("ReorderPoint", value); }
		}

		public Decimal TotalValue
		{
			get { return GetPropertyValue<Decimal>("TotalValue"); }
			set { SetPropertyValue("TotalValue", value); }
		}
		public InventoryProduct(): base(Session.DefaultSession)
		{
		}
		public InventoryProduct(Session session): base(session)
		{
		}
	}
}