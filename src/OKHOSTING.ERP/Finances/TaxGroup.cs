using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

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

		public ICollection<Tax> Taxes
		{
			get;
			set;
		}

		public decimal? GetTaxFor(decimal ammount)
		{
			decimal? totalTax = 0;

			foreach (Tax tax in Taxes)
			{
				totalTax = tax.GetTaxFor(ammount);
			}

			return totalTax;
		}
	}
}