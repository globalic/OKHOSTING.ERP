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


		public void CreateSale()
		{
			Sale sale = new Sale();
			sale.Customer = Customer;
			sale.Date = Date;

			foreach (InvoiceItem item in Items)
			{
				InvoiceItem newItem = new InvoiceItem();
				newItem.Product = item.Product;
				newItem.Price = item.Price;
				newItem.Quantity = item.Quantity;
				newItem.Description = item.Description;
				newItem.Discount = item.Discount;
				newItem.ProductInstance = item.ProductInstance;

				sale.Items.Add(newItem);
			}

			sale.Save();
		}
	}
}