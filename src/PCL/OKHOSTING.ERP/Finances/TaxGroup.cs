using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Finances
{
	/// <summary>
	/// A group of taxes that can be applied in a purchase or a sale
	/// </summary>
	public class TaxGroup
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		[StringLengthValidator(50)]
		public string Name
		{
			get;
			set;
		}

		public ICollection<TaxGroupItem> Taxes
		{
			get;
			set;
		}

		public decimal? GetTaxFor(decimal ammount)
		{
			decimal? totalTax = 0;

			foreach (Tax tax in Taxes.Select(i => i.Tax))
			{
				totalTax = tax.GetTaxFor(ammount);
			}

			return totalTax;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}