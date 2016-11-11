using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Vendors
{
	public class VendorCategory
	{
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