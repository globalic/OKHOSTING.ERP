using System;

namespace OKHOSTING.ERP.ORM
{
	/// <summary>
	/// Represents a loos relationship bewteen a company and any other object, which is considered
	/// <para xml:lang="es">
	/// Representa una relacion loos entre una compañia y cualquier otro objecto 
	/// </para>
	/// a company 'asset'
	/// </summary>
	public class CompanyAsset: OKHOSTING.ORM.Model.LooseForeignKey<Guid>
	{
		/// <summary>
		/// The company that owns the asset
		/// <para xml:lang="es">
		/// La compañia a la que pertenece el activo
		/// </para>
		/// </summary>
		public OKHOSTING.ERP.New.Company Company { get; set; }
	}
}