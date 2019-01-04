using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	public class PhoneCall : Conversation
	{
		/// <summary>
		/// The phone number that was dialed
		/// </summary>
		[StringLengthValidator(30)]
		public string PhoneNumber
		{
			get;
			set;
		}
	}
}