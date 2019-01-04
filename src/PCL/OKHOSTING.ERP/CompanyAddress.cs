using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// A location that belongs to a company
	/// <para xml:lang="es">
	/// Una ubicacion que pertenece a una empresa
	/// </para>
	/// </summary>
	public class CompanyAddress : Address
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