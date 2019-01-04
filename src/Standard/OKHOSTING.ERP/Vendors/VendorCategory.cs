using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Vendors
{
	public class VendorCategory
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
	}
}