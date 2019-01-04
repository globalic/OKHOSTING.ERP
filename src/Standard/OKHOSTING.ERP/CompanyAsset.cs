using OKHOSTING.Data.Validation;
using System;

namespace OKHOSTING.ERP.New.HR
{
	/// <summary>
	/// A company asset that is designated to an employee for use and/or supervision
	/// </summary>
	public class CompanyAsset
	{
		public Guid Id { get; set; }

		/// <summary>
		/// The asset that was designated to the employee. Can be any kind of object
		/// </summary>
		[RequiredValidator]
		public object Asset
		{
			get;
			set;
		}

		/// <summary>
		/// Estimated value of the asset
		/// </summary>
		public decimal Value
		{
			get;
			set;
		}

		/// <summary>
		/// Employee responsible for the asset
		/// </summary>
		public Employee AssignedTo
		{
			get;
			set;
		}
	}
}