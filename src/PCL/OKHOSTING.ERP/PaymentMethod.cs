using OKHOSTING.Data.Validation;
using System;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// A method of payment
	/// <para xml:lang="es">
	/// Un metodo de pago
	/// </para>
	/// </summary>
	public class PaymentMethod
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}
	}
}