using OKHOSTING.Data.Validation;
using OKHOSTING.ERP.HR;

namespace OKHOSTING.ERP.Customers
{
	/// <summary>
	/// A quote made for a customer
	/// </summary>
	public class Quote : Invoice
	{
		public SalesPerson SalesPerson
		{
            get;
            set;
		}

		public CommissionLevel CommissionLevel
		{
            get;
            set;
		}

		[RequiredValidator]
		public Customer Customer
		{
            get;
            set;
		}

		//public QuoteStatus Status
		//{
		//	get { return GetPropertyValue<QuoteStatus>("Status"); }
		//	set { SetPropertyValue("Status", value); }
		//}


		//public void CreateSale()
		//{
		//	Sale sale = new Sale(this.Session);
		//	sale.Customer = this.Customer;
		//	sale.Date = this.Date;

		//	foreach (InvoiceItem item in this.Items)
		//	{
		//		InvoiceItem newItem = new InvoiceItem(Session);
		//		newItem.Product = item.Product;
		//		newItem.Price = item.Price;
		//		newItem.Quantity = item.Quantity;
		//		newItem.Description = item.Description;

		//		sale.Items.Add(newItem);
		//	}

		//	sale.Save();
		//}
	}
}