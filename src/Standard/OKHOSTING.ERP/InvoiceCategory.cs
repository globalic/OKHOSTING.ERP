using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A category for invoices
	/// </summary>
	/// <remarks>Usefull to categorize invoices for customers anvendors, for internal control</remarks>
	public class InvoiceCategory
	{
		public Guid Id { get; set; }

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