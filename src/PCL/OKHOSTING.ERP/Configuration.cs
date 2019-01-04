namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// Defines my company information
	/// <para xml:lang="es">
	/// Define la informacion de mi empresa
	/// </para>
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// My company information
		/// <para xml:lang="es">
		/// La informacion de mi empresa
		/// </para>
		/// </summary>
		public Company MyCompany;

		/// <summary>
		/// Current configuration
		/// <para xml:lang="es">
		/// Configuracion actual
		/// </para>
		/// </summary>
		public static Configuration Current;

		/// <summary>
		/// Loads the current configuration
		/// <para xml:lang="es">
		/// Carga la configuracion actual
		/// </para>
		/// </summary>
		//static Configuration()
		//{
		//	Current = (Configuration) OKHOSTING.Tools.ConfigurationBase.Load(typeof(Configuration));
		//}
	}
}