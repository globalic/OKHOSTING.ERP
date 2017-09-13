using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Tools.Net.Mail;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Marketing.MailMarketing
{
	public class MailMarketingMessage
	{
		[StringLengthValidator(100)]
		[RequiredValidator]
		public string From
		{
			get;
			set;
		}

		[StringLengthValidator(100)]
		[RequiredValidator]
		public string Subject
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Content
		{
			get;
			set;
		}

		/// <summary>
		/// Group of companies who will receive this message
		/// </summary>
		public Group TargetGroup
		{
			get;

			set 
			{
				if (value != null && value.MemberType != null && !value.MemberType.IsSubclassOf(typeof(Company)) && !value.MemberType.Equals(typeof(Company)))
				{
					throw new ArgumentException("TargetGroup.MemberType must be Company or a subclass of Company");
				}

			}
		}

		public MarketingCampaign Campaign
		{
			get;
			set;
		}

		/*
		/// <summary>
		/// Sends the current message to the target group
		/// </summary>
		[Action]
		public void Send()
		{
			foreach (Company company in TargetGroup.Members)
			{
				foreach (CompanyContact contact in company.Contacts)
				{
					MailMarketingMessageTemplate template = new MailMarketingMessageTemplate();
					template.Message = this;
					template.Recipient = contact;
					OKHOSTING.Tools.Net.Mail.MailManager.Send(template);
				}
			}
		}
		*/

		public void Send(List<Company> companies)
		{
			foreach (Company company in companies)
			{
				foreach (CompanyContact contact in company.Contacts)
				{
					MailMarketingMessageTemplate template = new MailMarketingMessageTemplate();
					template.Message = this;
					template.Recipient = contact;
					OKHOSTING.Tools.Net.Mail.MailManager.Send(template);
				}
			}
		}

		public void Send(List<string> companies)
		{
			foreach (string s in companies)
			{
				MailMarketingMessageTemplate template = new MailMarketingMessageTemplate();
				template.Message = this;
				template.Recipient = null;
				template.To.Add(s);
				OKHOSTING.Tools.Net.Mail.MailManager.Send(template);
			}
		}
	}
}