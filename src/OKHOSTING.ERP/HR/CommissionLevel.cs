using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A percentage of commission that will be given to a sales person for a specific sale.
	/// Depending on the type of sale, different commission leves can be assigned to one sale
	/// </summary>
	public class CommissionLevel : ORM.PersistentClass<Guid>
	{ 
		/// <summary>
		/// Name of the commission level
		/// </summary>
		[StringLengthValidator(20)]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Percentage of commission that will be given to the salesperson on this level
		/// </summary>
		[RequiredValidator]
		public decimal Commission
		{
			get;
			set;
		}
	}
}