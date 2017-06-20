using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Finances
{
	/// <summary>
	/// A group of taxes that can be applied in a purchase or a sale
	/// </summary>
	public class TaxGroup : ORM.PersistentClass<Guid>
	{
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