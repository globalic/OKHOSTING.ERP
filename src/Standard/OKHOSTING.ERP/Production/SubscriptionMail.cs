using System;
using OKHOSTING.Tools.Net.Mail;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A mail template for subscription expiration notifications
	/// </summary>
	public class SubscriptionMail
	{
		/// <summary>
		/// Subscription wich expiration is being notified
		/// </summary>
		public Subscription Subscription;

		/*
		/// <summary>
		/// Replace all tags in the subject and body, and prepares the message to be sent
		/// </summary>
		public override void Init()
		{
			CompanyContact Recipient;

			if (Subscription.SoldTo == null)
			{
				Recipient = OKHOSTING.ERP.Configuration.Current.MyMainContact;
			}
			else
			{
				if (Subscription.SoldTo.Contacts.Count > 0)
				{
					Recipient = (CompanyContact)Subscription.SoldTo.Contacts[0];
				}
				else
				{
					Recipient = OKHOSTING.ERP.Configuration.Current.MyMainContact;
				}
			}

			//Subject = Translator.Current["Aviso de expiración"] + ": " + Subscription.Id;
			if (this.Subscription.Active)
			{
				Subject = string.Format("Por expirar: {0} - {1}", this.Subscription.Product.Name, Subscription.Name);
			}
			else
			{
				Subject = string.Format("Expirado: {0} - {1}", this.Subscription.Product.Name, Subscription.Name);
			}

			Priority = System.Net.Mail.MailPriority.High;

			//recipients
			To.Add(Recipient.Email);
			if (!string.IsNullOrWhiteSpace(Recipient.Email2)) CC.Add(Recipient.Email2);
			if (!string.IsNullOrWhiteSpace(Recipient.Company.Email)) CC.Add(Recipient.Company.Email);
			if (!string.IsNullOrWhiteSpace(Recipient.Company.Email2)) CC.Add(Recipient.Company.Email2);

			//recipient data
			base.ReplaceTag("Recipient.Prefix", Recipient.Prefix);
			base.ReplaceTag("Recipient.Alias", Recipient.Alias);
			base.ReplaceTag("Recipient.FirstName", Recipient.FirstName);
			base.ReplaceTag("Recipient.LastName", Recipient.LastName);
			base.ReplaceTag("Recipient.FullName", Recipient.FullName);
			base.ReplaceTag("Recipient.Email", Recipient.Email);
		
			//subscription data
			base.ReplaceTag("Subscription.Product", Subscription.Product.Name);
			base.ReplaceTag("Subscription.Name", Subscription.Name);
			base.ReplaceTag("Subscription.Start", Subscription.Start.ToLongDateString());
			base.ReplaceTag("Subscription.End", Subscription.End.Value.ToLongDateString());
			//base.ReplaceTag("Subscription.Active", Translator.Current[Subscription.Active]);
			base.ReplaceTag("Subscription.Product.Price", Subscription.Product.Price.ToString());
		}
		*/
	}
}