using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// A person in a company who we have contact with
	/// </summary>
	public class CompanyContact: Person
	{
		[RequiredValidator]
		public Company Company
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string Role
		{
			get;
			set;
		}
	}
}