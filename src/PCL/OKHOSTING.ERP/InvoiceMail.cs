using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.Tools.Net.Mail;

namespace OKHOSTING.ERP
{
	public abstract class InvoiceMail
	{
		/// <summary>
		/// Invoice that is being sent by email
		/// </summary>
		public Invoice Invoice;

		/// <summary>
		/// Replace all tags in the subject and body, and prepares the message to be sent
		/// </summary>
		/*
		public override void Init()
		{
			CompanyContact Recipient;

			Priority = System.Net.Mail.MailPriority.High;

			//recipients
			if (Invoice is Customers.Sale)
			{
				Customers.Customer customer = ((Customers.Sale)Invoice).Customer;
				Recipient = customer.Contacts[0];
				
				if(customer.Sales != null)
				{
					From = new System.Net.Mail.MailAddress(customer.SalesPerson.Email, customer.SalesPerson.FullName);
				}
			}
			else if (Invoice is Customers.Quote)
			{
				Recipient = ((Customers.Quote)Invoice).Customer.Contacts[0];
			}
			else
			{
				Recipient = OKHOSTING.ERP.Configuration.Current.MyMainContact;
			}
			
			To.Add(Recipient.Email);
			
			if (!string.IsNullOrWhiteSpace(Recipient.Email2)) CC.Add(Recipient.Email2);

			//recipient data
			base.ReplaceTag("Recipient.Prefix", Recipient.Prefix);
			base.ReplaceTag("Recipient.Alias", Recipient.Alias);
			base.ReplaceTag("Recipient.FirstName", Recipient.FirstName);
			base.ReplaceTag("Recipient.LastName", Recipient.LastName);
			base.ReplaceTag("Recipient.FullName", Recipient.FullName);
			base.ReplaceTag("Recipient.Email", Recipient.Email);

			//invoice data
			base.ReplaceTag("Invoice.Oid", Invoice.Oid.ToString());
			base.ReplaceTag("Invoice.AuxId", Invoice.AuxId);
			base.ReplaceTag("Invoice.Date", Invoice.Date.ToLongDateString());
			base.ReplaceTag("Invoice.Subtotal", Invoice.Subtotal.ToString());
			base.ReplaceTag("Invoice.Tax", Invoice.Tax.ToString());
			base.ReplaceTag("Invoice.Total", Invoice.Total.ToString());

			//items
			string items = string.Empty;
			foreach (InvoiceItem item in Invoice.Items)
			{
				items += string.Format
					(@"
					<tr>
						<td>{0}</td>
						<td>{1}</td>
						<td>{3}</td>
						<td>{4}</td>
						<td>{5}</td>
					</tr>
					",
					 item.Product,
					 item.Description,
					 item.Quantity,
					 item.Price,
					 item.Discount,
					 item.Total
					 );
			}

			base.ReplaceTag("Invoice.Items", items);
		}
		*/
	}
}