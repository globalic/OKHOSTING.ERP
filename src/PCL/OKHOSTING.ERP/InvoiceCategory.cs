using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// A category for invoices
	/// <para xml:lang="es">
	/// Una categoria para facturas
	/// </para>
	/// </summary>
	/// <remarks>Usefull to categorize invoices for customers and vendors, for internal control
	/// <para xml:lang="es">
	/// Muy util para categorizar facturas de clientes y vendedores, para un control interno
	/// </para>
	/// </remarks>
	public class InvoiceCategory
	{
		public Guid Id { get; set; }

		[RequiredValidator]
		[StringLengthValidator(50)]
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

		public override string ToString()
		{
			return Name;
		}
	}
}