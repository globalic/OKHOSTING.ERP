using System;

namespace OKHOSTING.ERP.ORM
{
	/// <summary>
	/// Represents a loos relationship bewteen a company and any other object, which is considered
	/// a company 'asset'
	/// </summary>
	public class CompanyAsset: OKHOSTING.ORM.Model.LooseForeignKey<Guid>
	{
		/// <summary>
		/// The company that owns the asset
		/// </summary>
		public OKHOSTING.ERP.New.Company Company { get; set; }
	}
}