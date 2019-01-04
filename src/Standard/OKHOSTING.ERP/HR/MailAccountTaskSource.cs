namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A mail account that will be crawled and Tasks will be generated from the inbox emails
	/// </summary>
	public class MailAccountTaskSource: TaskSource
	{
		/// <summary>
		/// Mail account where we will retrieve the tasks from
		/// </summary>
		//public MailAccount MailAccount
		//{
		//	get { return GetPropertyValue<MailAccount>("MailAccount"); }
		//	set { SetPropertyValue("MailAccount", value); }
		//}

		public override void Sync()
		{
			//copy all messages from source to a the local disk
			//ActiveUp.Net.Mail.Imap4Client source = new ActiveUp.Net.Mail.Imap4Client();
			//source.Connect(MailAccount.Host, MailAccount.Address, MailAccount.Password);
			//source.LoadMailboxes();
			
			//ActiveUp.Net.Mail.Mailbox inbox = source.AllMailboxes["INBOX"];
			//ActiveUp.Net.Mail.Mailbox procesed = source.AllMailboxes["INBOX.Procesed"];
			//if (procesed == null)
			//{
			//	source.CreateMailbox("INBOX.Procesed");
			//}

			//for (int i = 1; i <= inbox.MessageCount; i++)
			//{
			//	try
			//	{
			//		ActiveUp.Net.Mail.Message message = inbox.Fetch.MessageObject(i);
					
			//		CompanyMailMessage m = (CompanyMailMessage) typeof(CompanyMailMessage).CreateInstance(Session);
					
			//		m.Parse(message);
			//		m.Account = MailAccount;

			//		CompanyMailMessage duplicate = MyApplication.XpoSession.FindObject<CompanyMailMessage>(CriteriaOperator.And(new BinaryOperator("Subject", m.Subject), new BinaryOperator("From", m.From), new BinaryOperator("Date", m.Date.AddMinutes(10), BinaryOperatorType.Less), new BinaryOperator("Date", m.Date.AddMinutes(-10), BinaryOperatorType.Greater), new BinaryOperator("Oid", m.Oid, BinaryOperatorType.NotEqual)));
					
			//		if(duplicate != null)
			//		{
			//			m.Delete();
			//			m.Purge();
			//			inbox.MoveMessage(i, "INBOX.Procesed");
			//			continue;
			//		}

			//		m.Save();

			//		MailMessageTask task = new MailMessageTask(Session);
			//		task.Message = m;

			//		//get task auxid
			//		System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"#OK[\d+.]+");
			//		if (regex.IsMatch(m.Subject))
			//		{
			//			string auxId = regex.Match(m.Subject).Value.Replace(@"#OK", string.Empty);
			//			task.Parent = Session.FindObject<Task>("AuxId", auxId);
			//		}

			//		//auto assign
			//		if (m.Company == null)
			//		{
			//			//search for the employee with the less open tasks
			//			foreach(TaskSourceEmployee e in this.Employees)
			//			{
			//				if (task.AssignedTo == null || task.AssignedTo.OpenTasks.Count > e.Employee.OpenTasks.Count)
			//				{
			//					task.AssignedTo = e.Employee;
			//				}
			//			}
			//		}

			//		if (Employees.Count == 1)
			//		{
			//			task.AssignedTo = this.Employees[0].Employee;
			//		}

			//		task.Save();

			//		//move message
			//		inbox.MoveMessage(i, "INBOX.Procesed");
			//		i--;
			//	}
			//	catch (Exception e)
			//	{
			//		try
			//		{
			//			Log.Write("MailServer", e.ToString(), Log.HandledException);
			//		}
			//		catch { }
			//	}
			//}

			source.Disconnect();
		}
	}
}