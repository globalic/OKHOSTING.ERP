using OKHOSTING.Data.Validation;
using System;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A method of payment
	/// </summary>
	public class PaymentMethod : ORM.PersistentClass<Guid>
	{
		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}
	}
}