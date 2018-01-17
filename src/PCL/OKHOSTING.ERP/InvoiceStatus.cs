using System;
using OKHOSTING.Data.Validation;


namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// Tax applied to an invoice item
	/// </summary>
	public class InvoiceStatus
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Ammount being charged as tax
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(50)]
		public string Name
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}