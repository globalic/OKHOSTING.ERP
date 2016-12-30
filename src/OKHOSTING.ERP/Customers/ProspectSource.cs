using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Customers
{
	/// <summary>
	/// The source of a prospect, where the prospect was generated from. 
	/// Usefull to track marketing ROI
	/// </summary>
	/// <example>
	/// Samples of prospect sources are:
	/// Telemarketing
	/// Online Advertising
	/// Offline Advertising
	/// Friend's recommendation
	/// Incoming call
	/// Incoming email
	/// Incoming chat
	/// </example>
	public class ProspectSource
	{
		/// <summary>
		/// Name of the status
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(50)]
		public string Name
		{
			get;
			set;
		}
	}
}