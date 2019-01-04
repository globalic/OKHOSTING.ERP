namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// Defines my company information
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// My company information
		/// </summary>
		public Company MyCompany;

		/// <summary>
		/// Current configuration
		/// </summary>
		public static Configuration Current;

		/// <summary>
		/// Loads the current configuration
		/// </summary>
		//static Configuration()
		//{
		//	Current = (Configuration) OKHOSTING.Tools.ConfigurationBase.Load(typeof(Configuration));
		//}
	}
}