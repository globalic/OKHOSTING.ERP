using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Tools.Net.Mail;

namespace OKHOSTING.ERP.Marketing.MailMarketing
{
	public class MailMarketingMessageTemplate
	{
		public MailMarketingMessage Message { get; set; }
		public CompanyContact Recipient { get; set; }

		/*
		public override void Init()
		{
			Subject = Message.Subject;
			From = new System.Net.Mail.MailAddress(Message.From);
			
			//content
			ReplaceTag("Message.Content", Message.Content);

			//recipient
			if (Recipient != null)
			{
				To.Add(Recipient.Email);
				if (!string.IsNullOrWhiteSpace(Recipient.Email2)) CC.Add(Recipient.Email2);
				if (!string.IsNullOrWhiteSpace(Recipient.Company.Email)) CC.Add(Recipient.Company.Email);
				if (!string.IsNullOrWhiteSpace(Recipient.Company.Email2)) CC.Add(Recipient.Company.Email2);

				base.ReplaceTag("Recipient.Prefix", Recipient.Prefix);
				base.ReplaceTag("Recipient.Alias", Recipient.Alias);
				base.ReplaceTag("Recipient.FirstName", Recipient.FirstName);
				base.ReplaceTag("Recipient.LastName", Recipient.LastName);
				base.ReplaceTag("Recipient.FullName", Recipient.FullName);
				base.ReplaceTag("Recipient.Email", Recipient.Email);
			}
		}
		*/
	}
}