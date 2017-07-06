using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A location that belongs to a company
	/// </summary>
	public class CompanyLocation : Location
	{
		[RequiredValidator]
		public Company Company
		{
			get;
			set;
		}
	}
}