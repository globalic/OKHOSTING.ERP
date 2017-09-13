using OKHOSTING.Data.Validation;
using System;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A company department. A logical division in the company.
	/// </summary>
	/// <example>Management, HR, Marketing, Production, IT, etc.</example>
	public class Department : ORM.Model.Base<Guid>
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

		public Department Parent
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