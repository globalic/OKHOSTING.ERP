using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Tools.Net.Mail;

namespace OKHOSTING.ERP.HR
{
	public class TaskMail
	{
		/// <summary>
		/// Task wich is being notified
		/// </summary>
		public Task Task;

		/*
		/// <summary>
		/// Replace all tags in the subject and body, and prepares the message to be sent
		/// </summary>
		public override void Init()
		{
			Subject = "#OK<@Task.AuxId> - <@Task.Name>";
			Priority = System.Net.Mail.MailPriority.Normal;

			if (Task.Company == null)
			{
				throw new Exception("Task.Company can't be null");
			}

			if (Task.Company.Contacts.Count == 0)
			{
				throw new Exception("Task.Company nust have at least one Contact");
			}

			//recipients
			foreach (CompanyContact c in Task.Company.Contacts)
			{
				if (!string.IsNullOrWhiteSpace(c.Email)) CC.Add(c.Email);
				if (!string.IsNullOrWhiteSpace(c.Email2)) CC.Add(c.Email2);
			}

			if (!string.IsNullOrWhiteSpace(Task.Company.Email)) CC.Add(Task.Company.Email);
			if (!string.IsNullOrWhiteSpace(Task.Company.Email2)) CC.Add(Task.Company.Email2);

			//recipient data
			CompanyContact Recipient = Task.Company.Contacts[0];
			base.ReplaceTag("Recipient.Prefix", Recipient.Prefix);
			base.ReplaceTag("Recipient.Alias", Recipient.Alias);
			base.ReplaceTag("Recipient.FirstName", Recipient.FirstName);
			base.ReplaceTag("Recipient.LastName", Recipient.LastName);
			base.ReplaceTag("Recipient.FullName", Recipient.FullName);
			base.ReplaceTag("Recipient.Email", Recipient.Email);

			//Task data
			base.ReplaceTag("Task.AuxId", (Task.Parent == null) ? Task.AuxId : Task.Parent.AuxId);
			base.ReplaceTag("Task.Name", (Task.Parent == null) ? Task.Subject : Task.Parent.Subject);
			base.ReplaceTag("Task.Description", HtmlFormatter.ReplaceSpecialChars(Task.Description));
			base.ReplaceTag("Task.AssignedTo", Task.AssignedTo.Alias);
			base.ReplaceTag("Task.StartOn", Task.StartOn.ToLongDateString());
			base.ReplaceTag("Task.AssignedTo.MailSignature", Task.AssignedTo.MailSignature);
			base.ReplaceTag("Task.Oid", Task.Oid.ToString());

			//attachements
			foreach (TaskAttachement a in Task.Attachements)
			{
				string path = System.IO.Path.Combine(OKHOSTING.Tools.DefaultPaths.Custom, "Temp", a.File.FileName);
				System.IO.FileStream s = System.IO.File.OpenWrite(path);
				a.File.SaveToStream(s);
				s.Close();
				System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(path);
				base.Attachments.Add(att);
				//System.IO.File.Delete(path);
			}
		}*/
	}
}