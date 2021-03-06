using OKHOSTING.Data.Validation;
using System;
using System.Collections.Generic;

namespace OKHOSTING.ERP.New.Finances
{
	/// <summary>
	/// A tax that can be applied to sales or purchases
	/// </summary>
	public class Tax
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		[StringLengthValidator(50)]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Percentage that will be charged as tax
		/// </summary>
		/// <example>16 = 16%</example>
		/// <example>11.5 = 11.5%</example>
		public decimal Rate
		{
			get;
			set;
		}

		[RequiredValidator]
		public TaxAgency TaxAgency
		{
			get;
			set;
		}

		public ICollection<TaxGroupItem> TaxGroups
		{
			get;
			set;
		}

		public decimal? GetTaxFor(decimal? ammount)
		{
			if (ammount == null) return null;
			return ammount * Rate / 100;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}