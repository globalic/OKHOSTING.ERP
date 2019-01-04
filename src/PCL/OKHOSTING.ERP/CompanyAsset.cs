using OKHOSTING.Data.Validation;
using System;

namespace OKHOSTING.ERP.New.HR
{
	/// <summary>
	/// A company asset that is designated to an employee for use and/or supervision
	/// <para xml:lang="es">
	/// Un activo en la mepresa que es designado a un trabajor para uso y/o supervision
	/// </para>
	/// </summary>
	public class CompanyAsset
	{
		public Guid Id { get; set; }

		/// <summary>
		/// The asset that was designated to the employee. Can be any kind of object
		/// <para xml:lang="es">
		/// El activo que fue designado al trabajador. Puede ser cualquier tipo de objeto
		/// </para>
		/// </summary>
		[RequiredValidator]
		public object Asset
		{
			get;
			set;
		}

		/// <summary>
		/// Estimated value of the asset
		/// <para xml:lang="es">
		/// Valor estimado del activo
		/// </para>
		/// </summary>
		public decimal Value
		{
			get;
			set;
		}

		/// <summary>
		/// Employee responsible for the asset
		/// <para xml:lang="es">
		/// Trabajador responsable del activo
		/// </para>
		/// </summary>
		public Employee AssignedTo
		{
			get;
			set;
		}
	}
}