namespace OKHOSTING.ERP.New.IT
{
	/// <summary>
	/// Credenmtials that are assigned to an employee
	/// </summary>
	public class EmployeeCredentials
	{
		/// <summary>
		/// Unique Id
		/// </summary>
		public System.Guid Id { get; set; }

		/// <summary>
		/// Employee that has access to this credentials
		/// </summary>
		public HR.Employee Employee { get; set; }

		/// <summary>
		/// Credentials that have been assigned to othe employee
		/// </summary>
		public Security.Credentials Credentials { get; set; }
	}
}