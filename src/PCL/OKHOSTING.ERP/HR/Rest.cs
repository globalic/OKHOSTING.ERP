using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace OKHOSTING.ERP.HR
{
	[DefaultClassOptions]
	[System.ComponentModel.DefaultProperty("Name")]
	public class Rest: Activity
	{
		public Rest(): base(Session.DefaultSession)
		{
		}
		public Rest(Session session): base(session)
		{
		}
	}
}