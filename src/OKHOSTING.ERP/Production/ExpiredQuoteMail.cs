using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.ERP.Production;
using OKHOSTING.ERP.Customers;

namespace OKHOSTING.ERP.Production
{
	public class ExpiredQuoteMail : QuoteMail
	{
		/*
		/// <summary>
		/// Replace all tags in the subject and body, and prepares the message to be sent
		/// </summary>
		public override void Init()
		{
			CompanyContact Recipient;

			Priority = System.Net.Mail.MailPriority.High;

			//recipients
			Customers.Customer customer = ((Customers.Quote)Invoice).Customer;
			foreach (CompanyContact contact in customer.Contacts)
			{
				To.Add(contact.Email);
				if (!string.IsNullOrWhiteSpace(contact.Email2)) CC.Add(contact.Email2);
			}

			if (customer.SalesPerson != null)
			{
				ReplyToList.Add(new System.Net.Mail.MailAddress(customer.SalesPerson.Email, customer.SalesPerson.FullName));
			}
			
			Recipient = customer.Contacts[0];
			
			if (!string.IsNullOrWhiteSpace(Recipient.Company.Email)) CC.Add(Recipient.Company.Email);
			if (!string.IsNullOrWhiteSpace(Recipient.Company.Email2)) CC.Add(Recipient.Company.Email2);

			//recipient data
			ReplaceTag("Recipient.Prefix", Recipient.Prefix);
			ReplaceTag("Recipient.Alias", Recipient.Alias);
			ReplaceTag("Recipient.FirstName", Recipient.FirstName);
			ReplaceTag("Recipient.LastName", Recipient.LastName);
			ReplaceTag("Recipient.FullName", Recipient.FullName);
			ReplaceTag("Recipient.Email", Recipient.Email);

			//ReplaceTag("((Quote)this.Invoice).Customer.SalesPerson.FullName", ((Quote)this.Invoice).Customer.SalesPerson.FullName);
			//ReplaceTag("((Quote)this.Invoice).Customer.SalesPerson.Role", ((Quote)this.Invoice).Customer.SalesPerson.Role);
			//ReplaceTag("((Quote)this.Invoice).Customer.SalesPerson.Email", ((Quote)this.Invoice).Customer.SalesPerson.Email);

			//invoice data
			ReplaceTag("Invoice.Oid", Invoice.Oid.ToString());
			ReplaceTag("Invoice.AuxId", Invoice.AuxId);
			ReplaceTag("Invoice.Date", Invoice.Date.ToLongDateString());
			ReplaceTag("Invoice.Subtotal", Invoice.Subtotal.ToString("#0.00"));
			ReplaceTag("Invoice.Tax", Invoice.Tax.ToString("#0.00"));
			ReplaceTag("Invoice.Total", Invoice.Total.ToString("#0.00"));
			
			//items
			string items = string.Empty;
			foreach (InvoiceItem item in Invoice.Items)
			{
				Subscription s = MyApplication.XpoSession.FindObject<Subscription>(CriteriaOperator.And(new BinaryOperator("Product", item.Product), new BinaryOperator("Name", item.Description)));

				//remove subtotal and disccount to avoid confusion, only leave totals
				items += string.Format
					(@"
					<tr>
						<td>{0}</td>
						<td>{1}</td>
						<td>{2}</td>
						<td>{3}</td>
						<td>{4}</td>
					</tr>
					",
					 s.End.Value.ToShortDateString(),
					//((Subscription) item.ProductInstance).End.Value.ToShortDateString(),
					 item.Product,
					 item.Description,
					 item.Quantity,
					 item.Total.ToString("#0.00")
					 );
			}

			ReplaceTag("Invoice.Items", items);
		}
		*/
	}
}