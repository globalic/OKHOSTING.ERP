using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A scientific experiment, used to answer a question with a test
	/// </summary>
	public class Experiment : OKHOSTING.ERP.HR.Project
	{
		/// <summary>
		/// The inicial hypotesis, which is going to be tested. Should be accountable, accionable and auditable
		/// </summary>
		/// <example>
		/// 5% or more of current customers will buy an aditional domain if they are offered a relevant available domain for their business
		/// </example>
		public string Hypotesis
		{
			get;
			set;
		}
	}
}