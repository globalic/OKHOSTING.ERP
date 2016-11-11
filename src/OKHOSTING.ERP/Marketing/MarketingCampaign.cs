using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Marketing
{
	/// <summary>
	/// A company department. A logical division in the company.
	/// </summary>
	/// <example>Management, HR, Marketing, Production, IT, etc.</example>
	public class MarketingCampaign
	{
		[RequiredValidator]
		[StringLengthValidator(100)]
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