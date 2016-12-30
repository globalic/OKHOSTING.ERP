using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Customers
{
	/// <summary>
	/// The status of a prospect, each status represent a level in the sale pipeline
	/// </summary>
	/// <example>
	/// Some samples of pipeline prospect status are:
	/// New oportinuty
	/// Initial communication
	/// Fact finding
	/// Develop proposition
	/// Expose proposition
	/// Proposition evaluation
	/// Negotiation
	/// Close
	/// </example>
	public class ProspectStatus
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

		/// <summary>
		/// Order that this status ocuppies in the sale pipeline, starting from the top (wider) side of
		/// the pipeline and ending with the bottom (the narrowest) side of the pipeline
		/// </summary>
		public ushort PipeLineOrder
		{
			get;
			set;
		}
	}
}