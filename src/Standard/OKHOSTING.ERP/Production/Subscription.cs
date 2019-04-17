using System;
using OKHOSTING.Data.Validation;
using System.Linq;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Filters;
using System.Collections.Generic;
using OKHOSTING.Core;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// Represents a Subscription to a service, wether a sale or a purchase. Allows administrators
	/// to get control of customer service subscriptions and expirations, as well as vendor's
	/// </summary>
	public class Subscription: ProductInstance
	{
		/// <summary>
		/// Whether this subscription is currently active or not
		/// </summary>
		public bool Active
		{
			get 
			{ 
				return (Start <= System.DateTime.Now) && (End >= System.DateTime.Now || End == null);
			}
		}

		/// <summary>
		/// Starting date for the subscription
		/// </summary>
		[RequiredValidator]
		public DateTime Start
		{
			get;
			set;
		}

		/// <summary>
		/// Ending date for the subscription, date when the subsciption expires
		/// </summary>
		public DateTime? End
		{
			get;
			set;
		}

		/// <summary>
		/// Ending date for the grace period, 
		/// date when the subsciption is deleted and can't be renewed any more.
		/// If a grace period ends, the subscription is deleted and a new subscription must
		/// be created
		/// </summary>
		public DateTime? GracePeriodEnd
		{
			get;
			set;
		}

		/// <summary>
		/// Whether this subscription must be automatically renewed 
		/// without manual intervention and without prior notification
		/// </summary>
		[RequiredValidator]
		public bool AutoRenew
		{
			get;
			set;
		}

		/// <summary>
		/// Create / Renew the subscription when a new invoice has been created
		/// </summary>
		/// <param name="item">InvoiceItem that was created and that affects this subscription</param>
		protected override void OnInvoiceItem_AfterInsert(InvoiceItem item)
		{
			using (var db = BaitAndSwitch.Create<DataBase>())
			{
				Select<InvoiceItem> select = new Select<InvoiceItem>();
				select.AddMembers
				(
					m => m.Id,
					m => m.Discount,
					m => m.Description,
					m => m.Price,
					m => m.Quantity,
					m => m.Subtotal,
					m => m.Tax,
					m => m.Total,
					m => m.Invoice.Id,
					m => m.Invoice.Date,
					m => m.Invoice.InvoiceType,
					m => m.ProductInstance.Id,
					m => m.ProductInstance.Name,
					m => m.Product.Id,
					m => m.Product.ProductInstanceType
				);

				select.Where.Add(new ValueCompareFilter(select.DataType[m => m.Description], Name));

				var productType = DataType<Product>.GetDataType();
				var productInstanceTypeFilter = new ValueCompareFilter(productType[m => m.ProductInstanceType], item.Product.ProductInstanceType);
				productInstanceTypeFilter.TypeAlias = select.Joins.Where(j => j.Type.Equals(productType)).Single().Alias;
				select.Where.Add(productInstanceTypeFilter);

				var invoiceType = DataType<Invoice>.GetDataType();
				var order = new OrderBy(invoiceType[m => m.Date], Data.SortDirection.Ascending);
				order.TypeAlias = select.Joins.Where(j => j.Type.Equals(invoiceType)).Single().Alias;
				select.OrderBy.Add(order);

				//load subscription product
				var items = db.Select(select).ToArray();

				foreach (InvoiceItem i in items)
				{
					if (i.Invoice != null)
					{
						if (i.Invoice.InvoiceType == InvoiceType.Sale)
						{
							i.ProductInstance = this;
						}
						else
						{
							i.ProductInstance = null;
						}
					}
				}

				//load all items for this subscription and calculate end date
				foreach (InvoiceItem i in items)
				{
					if (i.Invoice.InvoiceType != InvoiceType.Sale)
					{
						continue;
					}

					SubscriptionProduct product = (SubscriptionProduct) db.SelectInherited(i.Product).LastOrDefault();

					if (Array.IndexOf(items, i) == 0)
					{
						//create new subscription
						Start = i.Invoice.Date;
						End = TimeUnit.Add(Start, (int) (product.SubscriptionLenght * i.Quantity), product.SubscriptionUnit);
					}
					else if (!IsUpgrade(i, items))
					{
						if (!DataType.HasPrimaryKey(this))
						{
							//create new subscription
							Start = i.Invoice.Date;
							End = TimeUnit.Add(Start, (int) (product.SubscriptionLenght * i.Quantity), product.SubscriptionUnit);
						}
						else
						{
							//renew existing subscription
							End = TimeUnit.Add(End.Value, (int) (product.SubscriptionLenght * i.Quantity), product.SubscriptionUnit);
						}
					}

					//grace period
					if (End == null)
					{
						GracePeriodEnd = null;
					}
					else
					{
						if (product.GracePeriodLenght == 0)
						{
							GracePeriodEnd = End;
						}
						else
						{
							GracePeriodEnd = TimeUnit.Add(End.Value, (int)(product.GracePeriodLenght * i.Quantity), product.GracePeriodUnit);
						}
					}
				}
			}
		}

		#region Notifications

		///// <summary>
		///// Sends an notification email to the customers 
		///// notifying the expiration date of this service
		///// </summary>
		//public void NotifyExpiration()
		//{
		//	SubscriptionMail mail;

		//	if (SoldTo == null)
		//	{
		//		Log.WriteDebug(typeof(Subscription).FullName + ".NotifyExpiration()", "Skipped: " + this);
		//		return;
		//	}

		//	//subscription is expired
		//	if (End < DateTime.Now)
		//	{
		//		mail = new ExpiredSubscriptionMail();
		//	}
		//	//subscription is about to expire
		//	else
		//	{
		//		mail = new ExpiringSubscriptionMail();
		//	}

		//	mail.Subscription = this;
		//	MailManager.Send(mail);

		//	Log.WriteDebug(typeof(Subscription).FullName + ".NotifyExpiration()", "Notified: " + this.ToString());
		//}

		///// <summary>
		///// Returns all subscriptions that are to expire in the specified time frame,
		///// which means we have to notify the subscriber about the expiration
		///// </summary>
		///// <param name="from">Starting expiration date to be used as filter</param>
		///// <param name="to">Ending expiration date to be used as filter</param>
		//public IList<Subscription> GetSubscriptionsExpiring(DateTime from, DateTime to)
		//{
		//	return GetSubscriptionsExpiring(from, to, null);
		//}

		///// <summary>
		///// Returns all subscriptions that are to expire in the specified time frame,
		///// which means we have to notify the subscriber about the expiration
		///// </summary>
		///// <param name="from">Starting expiration date to be used as filter</param>
		///// <param name="to">Ending expiration date to be used as filter</param>
		///// <param name="soldTo">
		///// If not null, only retrieves subscriptions that where sold to a specific customer
		///// </param>
		//public IList<Subscription> GetSubscriptionsExpiring(DateTime from, DateTime to, Customers.Customer soldTo)
		//{
		//	if (soldTo == null)
		//	{
		//		return new XPCollection<Subscription>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("End", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("End", to, BinaryOperatorType.LessOrEqual)));
		//	}
		//	else
		//	{
		//		return new XPCollection<Subscription>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("End", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("End", to, BinaryOperatorType.LessOrEqual), new BinaryOperator("SoldTo", soldTo), new BinaryOperator("Product", (object)null, BinaryOperatorType.NotEqual)));
		//	}
		//}

		///// <summary>
		///// Returns all quotes that are to expire in the specified time frame,
		///// which means we have to notify the subscriber about the expiration
		///// </summary>
		///// <param name="from">Starting expiration date to be used as filter</param>
		///// <param name="to">Ending expiration date to be used as filter</param>
		//public IList<Quote> GetQuotesExpiring(DateTime from, DateTime to)
		//{
		//	return GetQuotesExpiring(from, to, null);
		//}

		///// <summary>
		///// Returns all quotes that are to expire in the specified time frame,
		///// which means we have to notify the subscriber about the expiration
		///// </summary>
		///// <param name="from">Starting expiration date to be used as filter</param>
		///// <param name="to">Ending expiration date to be used as filter</param>
		///// <param name="soldTo">
		///// If not null, only retrieves subscriptions that where sold to a specific customer
		///// </param>
		//public IList<Quote> GetQuotesExpiring(DateTime from, DateTime to, Customer soldTo)
		//{
		//	if (soldTo == null)
		//	{
		//		return new XPCollection<Quote>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("Date", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("Date", to, BinaryOperatorType.LessOrEqual)));
		//	}
		//	else
		//	{
		//		return new XPCollection<Quote>(MyApplication.XpoSession, CriteriaOperator.And(new BinaryOperator("Date", from, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("Date", to, BinaryOperatorType.LessOrEqual), new BinaryOperator("SoldTo", soldTo)));
		//	}
		//}

		///// <summary>
		///// Creates and saves new Quotes containing all subscriptions that are about to expire in a specific time frame and for all customer
		///// </summary>
		///// <param name="from">Starting date of the time frame</param>
		///// <param name="to">Ending date of the time frame</param>
		//public void QuoteExpiringSubscriptions(DateTime from, DateTime to)
		//{
		//	XPCollection<Customer> customers = new XPCollection<Customer>(MyApplication.XpoSession);

		//	foreach (Customer c in customers)
		//	{
		//		if (!c.Active)
		//		{
		//			continue;
		//		}

		//		QuoteExpiringSubscriptions(from, to, c);
		//	}
		//}

		///// <summary>
		///// Creates and saves a new Quote containing all subscriptions that are about to expire in a specific time frame and for a specific customer
		///// </summary>
		///// <param name="from">Starting date of the time frame</param>
		///// <param name="to">Ending date of the time frame</param>
		///// <param name="customer">Customer which will have the quote created</param>
		//public void QuoteExpiringSubscriptions(DateTime from, DateTime to, Customer customer)
		//{
		//	IList<Subscription> subscriptions = GetSubscriptionsExpiring(from, to, customer);

		//	//if there are no expiring subscriptions, exit
		//	if (subscriptions.Count == 0) return;

		//	Quote quote = new Quote(MyApplication.XpoSession);

		//	quote.Customer = customer;
		//	quote.SalesPerson = customer.SalesPerson;
		//	quote.Notes = string.Empty;

		//	//assign the date of the first expiring service
		//	quote.Date = DateTime.MaxValue;

		//	//assign a default category
		//	quote.Category = new InvoiceCategory(MyApplication.XpoSession);
		//	//quote.Category.Oid = 1;

		//	foreach (Subscription s in subscriptions)
		//	{
		//		if (s.Product == null)
		//		{
		//			continue;
		//		}

		//		//omit duplicated hosting accounts
		//		bool duplicated = false;
		//		foreach (InvoiceItem i in quote.Items)
		//		{
		//			if (i.Description == s.Name && i.Product == s.Product)
		//			{
		//				duplicated = true;
		//				break;
		//			}
		//		}

		//		if (duplicated)
		//		{
		//			continue;
		//		}

		//		InvoiceItem item = new InvoiceItem(MyApplication.XpoSession);

		//		item.Invoice = quote;
		//		item.Description = s.Name;
		//		item.Product = s.Product;
		//		item.Price = s.Product.Price;
		//		item.Discount = 0;
		//		item.Quantity = 1;
		//		item.ProductInstance = s;

		//		//renovacion dominios .com.mx 
		//		//HARDCODED
		//		if (item.Product.Name.EndsWith(".com.mx") || item.Product.Name.EndsWith("org.mx") || item.Product.Name.EndsWith("net.mx"))
		//		{
		//			item.Price = item.Price * 2;
		//		}

		//		//assign the date of the first expiring service
		//		if (s.End < quote.Date)
		//		{
		//			quote.Date = s.End.Value;
		//		}

		//		quote.Notes += String.Format("{0} [{1}] expires: {2}\n", item.Product, item.Description, s.End.Value.ToShortDateString());

		//		//add taxes
		//		foreach (Tax t in s.Product.SaleTaxes.Taxes)
		//		{
		//			InvoiceItemTax itemTax = new InvoiceItemTax(MyApplication.XpoSession);
		//			itemTax.Tax = t;
		//			//itemTax.Amount = itemTax.Tax.GetTaxFor(item.Subtotal);
		//			item.Taxes.Add(itemTax);
		//		}

		//		quote.Items.Add(item);
		//	}

		//	if (quote.Items.Count > 0)
		//	{
		//		quote.Save();
		//	}
		//}

		///// <summary>
		///// Sends an notification email to all customers 
		///// who have services that are expired or about to expire
		///// </summary>
		///// <param name="from">
		///// Starting date for expirations to notufy
		///// </param>
		///// <param name="to">
		///// Ending date for expirations to notufy
		///// </param>
		//public void NotifyExpirationsSubscription(DateTime from, DateTime to)
		//{
		//	IList<Subscription> subscriptions = GetSubscriptionsExpiring(from, to);

		//	Log.Write(typeof(Subscription).FullName + ".NotifyExpirations(DateTime,DateTime)", "Loaded subscriptions: " + subscriptions.Count, Log.Debug);

		//	foreach (Subscription s in subscriptions)
		//	{
		//		try
		//		{
		//			s.NotifyExpiration();
		//		}
		//		catch (Exception e)
		//		{
		//			Log.Write(typeof(Subscription).FullName + ".NotifyExpirations(DateTime,DateTime)", e.ToString(), Log.Exception);
		//		}

		//		//System.Threading.Thread.Sleep(2000);
		//	}
		//}

		///// <summary>
		///// Sends an notification email to all customers 
		///// who have services that are expired or about to expire
		///// </summary>
		//public void NotifyExpirationsSubscription(int daysBeforeToday, int daysAfterToday)
		//{
		//	NotifyExpirations(DateTime.Now.AddDays(daysBeforeToday * -1), DateTime.Now.AddDays(daysAfterToday));
		//}

		///// <summary>
		///// Sends an notification email to all customers 
		///// who have services that are expired or about to expire
		///// </summary>
		///// <param name="from">
		///// Starting date for expirations to notufy
		///// </param>
		///// <param name="to">
		///// Ending date for expirations to notufy
		///// </param>
		//public void NotifyExpirations(System.DateTime from, System.DateTime to)
		//{
		//	IList<Quote> quotes = GetQuotesExpiring(from, to);
		//	NotifyExpirations(quotes);
		//}

		///// <summary>
		///// Sends an notification email to all customers 
		///// who have services that are expired or about to expire
		///// </summary>
		///// <param name="from">
		///// Starting date for expirations to notufy
		///// </param>
		///// <param name="to">
		///// Ending date for expirations to notufy
		///// </param>
		//public void NotifyExpirations(IList<Quote> quotes)
		//{

		//	Log.Write(typeof(Quote).FullName + ".NotifyExpirations(DateTime,DateTime)", "Loaded quotes: " + quotes.Count, Log.Debug);

		//	foreach (Quote q in quotes)
		//	{
		//		try
		//		{
		//			//omit customer-generated quotes
		//			if (string.IsNullOrWhiteSpace(q.Notes))
		//			{
		//				continue;
		//			}

		//			//remove already renewed items from quote
		//			for (int i = 0; i < q.Items.Count; i++)
		//			{
		//				InvoiceItem item = q.Items[i];

		//				if (item.ProductInstance == null)
		//				{
		//					//select from database, in case this instance already exist
		//					XPCollection instances = new XPCollection(PersistentCriteriaEvaluationBehavior.InTransaction, item.Session, item.Session.Dictionary.GetClassInfo(item.Product.ProductInstanceType), new BinaryOperator("Name", item.Description), true);
		//					instances.Sorting.Add(new SortProperty("Items.Count", DevExpress.Xpo.DB.SortingDirection.Descending));

		//					ProductInstance instance = null;

		//					if (instances.Count > 0)
		//					{
		//						instance = (ProductInstance)instances[0];

		//						if (instance.IsDeleted)
		//						{
		//							instance.Purge();
		//							instance = null;
		//						}

		//						item.ProductInstance = instance;
		//						item.Save();
		//					}
		//				}

		//				Subscription s = (Subscription)item.ProductInstance;

		//				//if remove from quote
		//				if (s.End > DateTime.Now.AddDays(60))
		//				{
		//					q.Items.Remove(item);
		//					i--;
		//				}
		//			}

		//			if (q.Items.Count == 0)
		//			{
		//				continue;
		//			}

		//			QuoteMail mail;

		//			//subscription is expired
		//			if (q.Date < DateTime.Now)
		//			{
		//				mail = new ExpiredQuoteMail();
		//				mail.Subject = "URGENTE Servicios expirados";
		//			}
		//			//subscription is about to expire
		//			else
		//			{
		//				mail = new ExpiringQuoteMail();
		//				mail.Subject = "Servicios por expirar";
		//			}

		//			mail.Invoice = q;
		//			MailManager.Send(mail);
		//		}
		//		catch (Exception e)
		//		{
		//			Log.Write(typeof(Quote).FullName + ".NotifyExpirations(DateTime,DateTime)", e.ToString(), Log.Exception);
		//		}

		//		//System.Threading.Thread.Sleep(2000);
		//	}
		//}

		///// <summary>
		///// Sends an notification email to all customers 
		///// who have services that are expired or about to expire
		///// </summary>
		//public void NotifyExpirations(int daysBeforeToday, int daysAfterToday)
		//{
		//	NotifyExpirations(DateTime.Now.AddDays(daysBeforeToday * -1), DateTime.Now.AddDays(daysAfterToday));
		//}

		///// <summary>
		///// Deletes all subscriptions that have a finished grace period
		///// </summary>
		///// <remarks>This method should run every day</remarks>
		//public void DeleteFinishedGracePeriodSubscriptions()
		//{
		//	IList<Subscription> expiredSubscriptions = MyApplication.ObjectSpace.GetObjects<Subscription>(new BinaryOperator("GracePeriodEnd", DateTime.Now, BinaryOperatorType.Less));

		//	foreach (Subscription s in expiredSubscriptions)
		//	{
		//		s.Delete();
		//	}
		//}

		#endregion

		/// <summary>
		/// Indicates if an invoice item was just a product upgrade and 
		/// no extention of the subscription expiration should be made
		/// </summary>
		public bool IsUpgrade(InvoiceItem item, InvoiceItem[] items)
		{
			if (item.ProductInstance != this)
			{
				throw new ArgumentException("Item should belong to Items collection", "item");
			}

			//we assume that if the item is not the first one, and the pricex is much lower than the renewal price, 
			//and the product is different than the last item's product, we can safetly say this is a product upgrade
			var index = Array.IndexOf(items, item);
			SubscriptionProduct sp = item.Product as SubscriptionProduct;
			SubscriptionProduct spOld = items[index - 1].Product as SubscriptionProduct;
			double days = item.Invoice.Date.Subtract(items[index - 1].Invoice.Date).TotalDays;

			return
				Array.IndexOf(items, item) > 0 &&
				item.Subtotal < item.Product.Price &&
				sp.Price > spOld.Price &&
				sp != spOld &&
				sp.SubscriptionLenght == spOld.SubscriptionLenght &&
				sp.SubscriptionUnit == spOld.SubscriptionUnit &&
				days < 240 &&
				(
					(
						days > 180 &&
						item.Subtotal <= (sp.Price / 2)
					)
					||
					(
						days < 180 &&
						item.Subtotal < sp.Price
					)
				);
		}
	}
}