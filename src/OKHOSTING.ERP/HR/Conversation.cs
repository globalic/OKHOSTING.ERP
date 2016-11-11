using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	public class Conversation: Task
	{
		[RequiredValidator]
		public ConversationOrigin Origin
		{
            get;
            set;
		}

		/// <summary>
		/// Person who sent/received this conversation on the other end
		/// </summary>
		public CompanyContact Contact
		{
            get;
            set;
		}

		public enum ConversationOrigin
		{
			/// <summary>
			/// The other person called us
			/// </summary>
			Inbound,

			/// <summary>
			/// We called the other person
			/// </summary>
			Outboud,
		}

	}
}