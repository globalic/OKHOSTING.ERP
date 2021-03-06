using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Finances
{
	/// <summary>
	/// A tax agency which taxes must be paid to
	/// </summary>
	public class TaxAgency
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string Telephone
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string Fax
		{
			get;
			set;
		}

		[StringLengthValidator(200)]
		public string Url
		{
			get;
			set;
		}

		[StringLengthValidator(200)]
		public string Email
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Notes
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