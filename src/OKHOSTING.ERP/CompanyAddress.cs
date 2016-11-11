using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// An address that belongs to a company
	/// </summary>
	public class CompanyAddress: Address
	{
		[RequiredValidator]
		public Company Company
		{
			get;
			set;
		}

		[RequiredValidator]
		public CompanyAddressType AddressType
		{
			get;
			set;
		}

	}
}