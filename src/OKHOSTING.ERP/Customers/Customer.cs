using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;
using OKHOSTING.ERP.HR;
using OKHOSTING.ERP.Production;

namespace OKHOSTING.ERP.Customers
{
	/// <summary>
	/// A company that purchases products or services
	/// </summary>
	public class Customer : Company
	{
		public SalesPerson SalesPerson
		{
            get;
            set;
		}

		[RequiredValidator]
		public Currency Currency
		{
            get;
            set;
		}
		
		public CustomerCategory Category
		{
            get;
            set;
		}

		/// <summary>
		/// Indicates how the customer found us
		/// </summary>
		[StringLengthValidator(100)]
		public string HowDidYouFindUs
		{
            get;
            set;
		}

		public string SoldProductsString
		{
			get 
			{
				string names = string.Empty;

				foreach (ProductInstance product in SoldProducts)
				{
					names += product.Name + ',' + ' ';
				}

				names = names.Trim(',', ' ');
				
				return names;
			}
		}

		public DateTime RegisteredSince
		{
            get;
            set;
		}

		#region Calculated fields

		/// <summary>
		/// Total ammount sold to the customer so far, including taxes
		/// </summary>
		public decimal TotalSold
		{
			get
			{
				return Sales.Sum(s => s.Total);
			}
		}

		/// <summary>
		/// Number of sales made to the customer so far
		/// </summary>
		public int TotalSales
		{
			get
			{
				return Sales.Count;
			}
		}

		/// <summary>
		/// Total balance of the customer so far, including taxes
		/// </summary>
		public decimal Balance
		{
			get
			{
				return Sales.Sum(s => s.Balance);
			}
		}

		/// <summary>
		/// Date of the first invoice
		/// </summary>
		public DateTime? FirstSaleDate
		{
			get
			{
				return Sales.Min(s => s.Date);
			}
		}

		/// <summary>
		/// Date of the first invoice
		/// </summary>
		public DateTime? LastSaleDate
		{
			get
			{
				return Sales.Max(s => s.Date);
			}
		}

		/// <summary>
		/// Indicates if the customer is still active
		/// </summary>
		public bool Active
		{
			get
			{
				if (TotalSales > 0)
				{
					return DateTime.Today.Subtract(LastSaleDate.Value).TotalDays < 365 || SoldProducts.Count > 0;
				}
				else
				{
					return false;
				}
			}
		}

		#endregion

		#region Collections

		public ICollection<Sale> Sales
		{
            get;
            set;
		}

		public ICollection<Quote> Quotes
		{
            get;
            set;
		}

		public ICollection<Production.ProductInstance> SoldProducts
		{
            get;
            set;
		}

		#endregion

		/// <summary>
		/// Merges another Customer with the current Customer. Transfers all data from the other Customer into the current one,
		/// including invoices, contacts, addresses, etc.
		/// Re-assigns all foreign-key related data from the other Customer in favor of the current one
		/// and deletes the merged Customer at the end.
		/// </summary>
		/// <remarks>
		/// The merged Customer will be deleted. Customer properties will not be copied into the current Customer, only foreign-key related DataObjects will 
		/// be reasigned to the current one
		/// </remarks>
		/// <param name="customer">Customer that willl be merged and deleted</param>
		//public void Merge(Customer customer)
		//{
		//	if (customer.Oid == this.Oid)
		//	{
		//		throw new ArgumentException("Can't merge the same customer", "customer");
		//	}

		//	while (customer.Sales.Count > 0)
		//	{
		//		Sale s = customer.Sales[0];
		//		s.Customer = this;
		//		s.Save();
		//	}

		//	while (customer.Quotes.Count > 0)
		//	{
		//		Quote s = customer.Quotes[0];
		//		s.Customer = this;
		//		s.Save();
		//	}

		//	while (customer.Contacts.Count > 0)
		//	{
		//		CompanyContact s = customer.Contacts[0];
		//		s.Company = this;
		//		s.Save();
		//	}

		//	while (customer.Addresses.Count > 0)
		//	{
		//		CompanyAddress s = customer.Addresses[0];
		//		s.Company = this;
		//		s.Save();
		//	}

		//	while (customer.SoldProducts.Count > 0)
		//	{
		//		ProductInstance s = customer.SoldProducts[0];
		//		s.SoldTo = this;
		//		s.Save();
		//	}

		//	//delete the other customer
		//	customer.Delete();
		//}
	}
}