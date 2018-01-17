using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.New.Finances
{
	/// <summary>
	/// A group of taxes that can be applied in a purchase or a sale
	/// </summary>
	public class TaxGroupItem
	{
		public Guid Id { get; set; }

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