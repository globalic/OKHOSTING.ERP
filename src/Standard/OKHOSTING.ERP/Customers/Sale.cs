using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.New.Customers
{
	/// <summary>
	/// A sale made for a customer
	/// </summary>
	public class Sale : Invoice
	{
		public SalesPerson SalesPerson
		{
			get;
			set;
		}

		public CommissionLevel CommissionLevel
		{
			get;
			set;
		}

		[RequiredValidator]
		public Customer Customer
		{
			get;
			set;
		}
	}
}