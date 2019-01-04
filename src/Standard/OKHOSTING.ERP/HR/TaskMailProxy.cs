using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Tools;
using OKHOSTING.Tools.Extensions;
using OKHOSTING.Data.Web.Mail;

namespace OKHOSTING.ERP.HR
{
	public class TaskMailProxy
	{
		public List<MailMessage> RetrieveMessages()
		{
			foreach (Guid iod in Configuration.Current.MailSources)
			{
				MailAccount acc = MyApplication.XpoSession.FindObject<MailAccount>("Oid", iod);
				acc.BackupToDataBase();
				
				foreach (MailMessage m in acc.Messages)
				{
					ActiveUp.Net.Mail.Message message = client.RetrieveMessageObject(i, false);
					MailMessage mail;

					//is this a new ticket or a response to an existing one?
					if (message.Subject.Contains("OK#"))
					{
						mail = Session.FindObject<MailMessage>("TicketId", "645604566540456");

						mail.Description += "\n\n" + message.BodyText.TextStripped;
					}
					else
					{
						mail = new MailMessage();
						mail.Name = message.Subject;
						mail.Description = message.BodyText.TextStripped;
						mail.StartDate = message.Date;

						CompanyContact contact = MyApplication.XpoSession.FindObject<CompanyContact>(CriteriaOperator.Or(new BinaryOperator("Email", message.From.Email), new BinaryOperator("Email2", message.From.Email)));
						mail.Company = contact.Company;
					}

					result.Add(mail);
				}
			}

			return result;
		}

		public class Configuration : ConfigurationBase
		{
			public Guid[] MailSources;

			/// <summary>
			/// Current configuration
			/// </summary>
			public static Configuration Current;

			/// <summary>
			/// Loads the current configuration
			/// </summary>
			static Configuration()
			{
				Current = (Configuration) OKHOSTING.Tools.ConfigurationBase.Load(typeof(Configuration));
			}
		}
	}
}