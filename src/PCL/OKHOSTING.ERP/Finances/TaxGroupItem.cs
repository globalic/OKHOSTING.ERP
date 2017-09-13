using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Finances
{
	/// <summary>
	/// A group of taxes that can be applied in a purchase or a sale
	/// </summary>
	public class TaxGroupItem : ORM.Model.Base<Guid>
	{
		[RequiredValidator]
		public Tax Tax
		{
			get;
			set;
		}

		public TaxGroup Group
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Tax + "-" + Group;
		}
	}
}