using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Data.Web.Mail;
using OKHOSTING.Tools;
using OKHOSTING.Tools.Extensions;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A task that is created from a MailMessage that was sent or received
	/// </summary>
	public class MailMessageTask : Conversation
	{
		[RequiredValidator]
		public CompanyMailMessage Message
		{
			get;
			set;
		}
	}
}