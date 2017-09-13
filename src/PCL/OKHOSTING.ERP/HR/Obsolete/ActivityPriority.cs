using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	public class ActivityPriority
	{
		[RequiredValidator]
		[StringLengthValidator(20)]
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