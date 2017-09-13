using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using OKHOSTING.Tools;
using OKHOSTING.Tools.Net.Mail;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using OKHOSTING.ERP.Customers;
using OKHOSTING.ERP.Finances;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// Represents a Subscription to a service, wether a sale or a purchase. Allows administrators
	/// to get control of customer service subscriptions and expirations, as well as vendor's
	/// </summary>
	[NavigationItem]
	public class Subscription : ProductInstance
	{
		/// <summary>
		/// Whether this subscription is currently active or not
		/// </summary>
		public Boolean Active
		{
			get 
			{ 
				return (Start <= System.DateTime.Now) && (End >= System.DateTime.Now || End == null);
			}
		}

		/// <summary>
		/// Starting date for the subscription
		/// </summary>
		[RuleRequiredField]
		public DateTime Start
		{
			get { return GetPropertyValue<DateTime>("Start"); }
			set { SetPropertyValue("Start", value); }
		}

		/// <summary>
		/// Ending date for the subscription, date when the subsciption expires
		/// </summary>
		public DateTime? End
		{
			get { return GetPropertyValue<DateTime?>("End"); }
			set { SetPropertyValue("End", value); }
		}

		/// <summary>
		/// Ending date for the grace period, 
		/// date when the subsciption is deleted and can't be renewed any more.
		/// If a grace period ends, the subscription is deleted and a new subscription must
		/// be created
		/// </summary>
		public DateTime? GracePeriodEnd
		{
			get { return GetPropertyValue<DateTime?>("GracePeriodEnd"); }
			set { SetPropertyValue("GracePeriodEnd", value); }
		}

		/// <summary>
		/// Whether this subscription must be automatically renewed 
		/// without manual intervention and without prior notification
		/// </summary>
		[RuleRequiredField]
		public Boolean AutoRenew
		{
			get { return GetPropertyValue<Boolean>("AutoRenew"); }
			set { SetPropertyValue("AutoRenew", value); }
		}

		public Subscription()
		{
		}

		public Subscription(DevExpress.Xpo.Session session): base(session)
		{
		}

		/// <summary>
		/// Create / Renew the subscription when a new invoice has been created
		/// </summary>
		/// <param name="item">InvoiceItem that was created and that affects this subscription</param>
		protected override void OnInvoiceItem_AfterInsert(InvoiceItem item)
		{
			base.OnInvoiceItem_AfterInsert(item);

			//load subscription product
			SubscriptionProduct product = (SubscriptionProduct)item.Product;

			//renew existing subscription
			if (!IsNewObject() && End != null)
			{
				End = TimeUnit.Add(End.Value, (int)(product.SubscriptionLenght * item.Quantity), product.SubscriptionUnit);
			}

			//create new subscription
			else
			{
				Start = item.Invoice.Date;
				End = TimeUnit.Add(Start, (int)(product.SubscriptionLenght * item.Quantity), product.SubscriptionUnit);
			}

			//grace period
			if (End == null)
			{
				GracePeriodEnd = null;
			}
			else
			{
				if (product.GracePeriodLenght == null || product.GracePeriodUnit == null)
				{
					GracePeriodEnd = End;
				}
				else
				{
					GracePeriodEnd = TimeUnit.Add(End.Value, (int)(product.GracePeriodLenght * item.Quantity), product.GracePeriodUnit);
				}
			}
		}

		#region Notifications

		/// <summary>
		/// Sends an notification email to the customers 
		/// notifying the expiration date of this service
		/// </summary>
		public void NotifyExpiration()
		{
			SubscriptionMail mail;

			if (SoldTo == null)
			{
				Log.WriteDebug(typeof(Subscription).FullName + ".NotifyExpiration()", "Skipped: " + this);
				return;
			}

			//subscription is expired
			if (End < DateTime.Now)
			{
				mail = new ExpiredSubscriptionMail();
			}
			//subscription is about to expire
			else
			{
				mail = new ExpiringSubscriptionMail();
			}

			mail.Subscription = this;
			MailManager.Send(mail);

			Log.WriteDebug(typeof(Subscription).FullName + ".NotifyExpiration()", "Notified: " + this.ToString());
		}

		/// <summary>
		/// Returns all subscriptions that are to expire in the specified time frame,
		/// which means we have to notify the subscriber about the expiration
		/// </summary>
		/// <param name="from">Starting expiration date to be used as filter</param>
		/// <param name="to">Ending expiration date to be used as filter</param>
		public static IList<Subscription> GetSubscriptionsExpiring(DateTime from, DateTime to)
		{
			return GetSubscriptionsExpiring(from, to, null);
		}

		/// <summary>
		/// Returns all subscriptions that are to expire in the specified time frame,
		/// which means we have to notify the subscriber about the expiration
		/// </summary>
		/// <param name="from">Starting expiration date to be used as filter</param>
		/// <param name="to">Ending expiration date to be used as filter</param>
		/// <param name="soldTo">
		/// If not null, only retrieves subscriptions that where sold to a specific customer
		/// </param>
		public static IList<Subscription> GetSubscriptionsExpiring(DateTime from, DateTime to, Customer soldTo)
		{
			if (soldTo == null)
			{
				return new XPCollection<Subscription>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("End", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("End", to, BinaryOperatorType.LessOrEqual)));
			}
			else
			{
				return new XPCollection<Subscription>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("End", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("End", to, BinaryOperatorType.LessOrEqual), new BinaryOperator("SoldTo", soldTo), new BinaryOperator("Product", (object) null, BinaryOperatorType.NotEqual)));
			}
		}

		/// <summary>
		/// Returns all quotes that are to expire in the specified time frame,
		/// which means we have to notify the subscriber about the expiration
		/// </summary>
		/// <param name="from">Starting expiration date to be used as filter</param>
		/// <param name="to">Ending expiration date to be used as filter</param>
		public static IList<Quote> GetQuotesExpiring(DateTime from, DateTime to)
		{
			return GetQuotesExpiring(from, to, null);
		}

		/// <summary>
		/// Returns all quotes that are to expire in the specified time frame,
		/// which means we have to notify the subscriber about the expiration
		/// </summary>
		/// <param name="from">Starting expiration date to be used as filter</param>
		/// <param name="to">Ending expiration date to be used as filter</param>
		/// <param name="soldTo">
		/// If not null, only retrieves subscriptions that where sold to a specific customer
		/// </param>
		public static IList<Quote> GetQuotesExpiring(DateTime from, DateTime to, Customer soldTo)
		{
			if (soldTo == null)
			{
				return new XPCollection<Quote>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("Date", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("Date", to, BinaryOperatorType.LessOrEqual)));
			}
			else
			{
				return new XPCollection<Quote>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("Date", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("Date", to, BinaryOperatorType.LessOrEqual), new BinaryOperator("SoldTo", soldTo)));
			}
		}

		/// <summary>
		/// Creates and saves new Quotes containing all subscriptions that are about to expire in a specific time frame and for all customer
		/// </summary>
		/// <param name="from">Starting date of the time frame</param>
		/// <param name="to">Ending date of the time frame</param>
		public static void QuoteExpiringSubscriptions(DateTime from, DateTime to)
		{
			XPCollection<Customer> customers = new XPCollection<Customer>(MyApplication.XpoSession);

			foreach (Customer c in customers)
			{
				QuoteExpiringSubscriptions(from, to, c);
			}
		}

		/// <summary>
		/// Creates and saves a new Quote containing all subscriptions that are about to expire in a specific time frame and for a specific customer
		/// </summary>
		/// <param name="from">Starting date of the time frame</param>
		/// <param name="to">Ending date of the time frame</param>
		/// <param name="customer">Customer which will have the quote created</param>
		public static void QuoteExpiringSubscriptions(DateTime from, DateTime to, Customer customer)
		{
			IList<Subscription> subscriptions = GetSubscriptionsExpiring(from, to, customer);

			//if there are no expiring subscriptions, exit
			if (subscriptions.Count == 0) return;

			Quote quote = new Quote(MyApplication.XpoSession);

			quote.Customer = customer;
			quote.SalesPerson = customer.SalesPerson;
			quote.Notes = string.Empty;

			//assign the date of the first expiring service
			quote.Date = DateTime.MaxValue;

			//assign a default category
			quote.Category = new InvoiceCategory(MyApplication.XpoSession);
			//quote.Category.Oid = 1;

			foreach (Subscription s in subscriptions)
			{
				if (s.Product == null)
				{
					continue;
				}

				InvoiceItem item = new InvoiceItem(MyApplication.XpoSession);

				item.Invoice = quote;
				item.Description = s.Name;
				item.Product = s.Product;
				item.Price = s.Product.Price;
				item.Discount = 0;
				item.Quantity = 1;
				item.ProductInstance = s;

				//renovacion dominios .com.mx 
				//HARDCODED
				if (item.Product.Name == "Dominio .com.mx")
				{
					item.Price = item.Price * 2;
				}

				//assign the date of the first expiring service
				if (s.End < quote.Date)
				{
					quote.Date = s.End.Value;
				}

				quote.Notes += String.Format("{0} [{1}] expires: {2}\n", item.Product, item.Description, s.End.Value.ToShortDateString());

				//add taxes
				foreach (Tax t in s.Product.SaleTaxes.Taxes)
				{
					InvoiceItemTax itemTax = new InvoiceItemTax(MyApplication.XpoSession);
					itemTax.Tax = t;
					//itemTax.Amount = itemTax.Tax.GetTaxFor(item.Subtotal);
					item.Taxes.Add(itemTax);
				}

				quote.Items.Add(item);
			}

			if (quote.Items.Count > 0)
			{
				quote.Save();
			}
		}

		/// <summary>
		/// Sends an notification email to all customers 
		/// who have services that are expired or about to expire
		/// </summary>
		/// <param name="from">
		/// Starting date for expirations to notufy
		/// </param>
		/// <param name="to">
		/// Ending date for expirations to notufy
		/// </param>
		public static void NotifyExpirationsSubscription(DateTime from, DateTime to)
		{
			IList<Subscription> subscriptions = GetSubscriptionsExpiring(from, to);

			Log.Write(typeof(Subscription).FullName + ".NotifyExpirations(DateTime,DateTime)", "Loaded subscriptions: " + subscriptions.Count, Log.Debug);

			foreach (Subscription s in subscriptions)
			{
				try
				{
					s.NotifyExpiration();
				}
				catch (Exception e)
				{
					Log.Write(typeof(Subscription).FullName + ".NotifyExpirations(DateTime,DateTime)", e.ToString(), Log.Exception);
				}

				//System.Threading.Thread.Sleep(2000);
			}
		}

		/// <summary>
		/// Sends an notification email to all customers 
		/// who have services that are expired or about to expire
		/// </summary>
		public static void NotifyExpirationsSubscription(int daysBeforeToday, int daysAfterToday)
		{
			NotifyExpirations(DateTime.Now.AddDays(daysBeforeToday * -1), DateTime.Now.AddDays(daysAfterToday));
		}

		/// <summary>
		/// Sends an notification email to all customers 
		/// who have services that are expired or about to expire
		/// </summary>
		/// <param name="from">
		/// Starting date for expirations to notufy
		/// </param>
		/// <param name="to">
		/// Ending date for expirations to notufy
		/// </param>
		public static void NotifyExpirations(DateTime from, DateTime to)
		{
			IList<Quote> quotes = GetQuotesExpiring(from, to);
			NotifyExpirations(quotes);
		}

		/// <summary>
		/// Sends an notification email to all customers 
		/// who have services that are expired or about to expire
		/// </summary>
		/// <param name="from">
		/// Starting date for expirations to notufy
		/// </param>
		/// <param name="to">
		/// Ending date for expirations to notufy
		/// </param>
		public static void NotifyExpirations(IList<Quote> quotes)
		{

			Log.Write(typeof(Quote).FullName + ".NotifyExpirations(DateTime,DateTime)", "Loaded quotes: " + quotes.Count, Log.Debug);

			foreach (Quote q in quotes)
			{
				try
				{
					//omit customer-generated quotes
					if (string.IsNullOrWhiteSpace(q.Notes))
					{
						continue;
					}

					QuoteMail mail;

					//subscription is expired
					if (q.Date < DateTime.Now)
					{
						mail = new ExpiredQuoteMail();
						mail.Subject = "URGENTE Servicios expirados";
					}
					//subscription is about to expire
					else
					{
						mail = new ExpiringQuoteMail();
						mail.Subject = "Servicios por expirar";
					}

					mail.Invoice = q;
					MailManager.Send(mail);
				}
				catch (Exception e)
				{
					Log.Write(typeof(Quote).FullName + ".NotifyExpirations(DateTime,DateTime)", e.ToString(), Log.Exception);
				}

				//System.Threading.Thread.Sleep(2000);
			}
		}

		/// <summary>
		/// Sends an notification email to all customers 
		/// who have services that are expired or about to expire
		/// </summary>
		public static void NotifyExpirations(int daysBeforeToday, int daysAfterToday)
		{
			NotifyExpirations(DateTime.Now.AddDays(daysBeforeToday * -1), DateTime.Now.AddDays(daysAfterToday));
		}

		/// <summary>
		/// Deletes all subscriptions that have a finished grace period
		/// </summary>
		/// <remarks>This method should run every day</remarks>
		public static void DeleteFinishedGracePeriodSubscriptions()
		{
			IList<Subscription> expiredSubscriptions = MyApplication.ObjectSpace.GetObjects<Subscription>(new BinaryOperator("GracePeriodEnd", DateTime.Now, BinaryOperatorType.Less));

			foreach (Subscription s in expiredSubscriptions)
			{
				s.Delete();
			}
		}

		#endregion
	}
}