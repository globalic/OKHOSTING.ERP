namespace OKHOSTING.ERP
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
		/// My billing address
		/// </summary>
		public CompanyAddress MyBillingAddress;
		
		/// <summary>
		/// My shipping address
		/// </summary>
		public CompanyAddress MyShippingAddress;

		/// <summary>
		/// My company's main contact
		/// </summary>
		public CompanyContact MyMainContact;
		
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