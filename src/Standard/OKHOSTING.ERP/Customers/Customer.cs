using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;
using OKHOSTING.ERP.New.Production;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM.Model;

namespace OKHOSTING.ERP.New.Customers
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

		public DateTime RegisteredSince
		{
			get;
			set;
		}

		#region Calculated fields

		public string SoldProductsString
		{
			get
			{
				string names = string.Empty;

				foreach (ProductInstance product in SoldProducts)
				{
					names += product.Product.Name + ',' + ' ';
				}

				names = names.Trim(',', ' ');

				return names;
			}
		}

		/// <summary>
		/// Total ammount sold to the customer so far, including taxes
		/// </summary>
		public decimal TotalSold
		{
			get;
			set;
		}

		/// <summary>
		/// Number of sales made to the customer so far
		/// </summary>
		public int TotalSales
		{
			get;
			set;
		}

		/// <summary>
		/// Total balance of the customer so far, including taxes
		/// </summary>
		public decimal Balance
		{
			get;
			set;
		}

		/// <summary>
		/// Date of the first invoice
		/// </summary>
		public DateTime? FirstSaleDate
		{
			get;
			set;
		}

		/// <summary>
		/// Date of the first invoice
		/// </summary>
		public DateTime? LastSaleDate
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates if the customer is still active
		/// </summary>
		public bool Active
		{
			get;
			set;
		}

		#endregion

		#region Collections

		public ICollection<Sale> Sales
		{
			get;
			set;
		}

		public ICollection<ProductInstance> SoldProducts
		{
			get;
			set;
		}

		#endregion

		/// <summary>
		/// Calculates current customer's balance
		/// </summary>
		public void CalculateStatistics()
		{
			TotalSold = 0;
			TotalSales = 0;
			Balance = 0;
			FirstSaleDate = null;
			LastSaleDate = null;

			foreach (Sale sale in Sales)
			{
				sale.CalculateTotals();
				TotalSold += sale.Total;
				TotalSales++;
				Balance += sale.Balance;

				if (FirstSaleDate == null || sale.Date < FirstSaleDate) FirstSaleDate = sale.Date;
				if (LastSaleDate == null || sale.Date > LastSaleDate) LastSaleDate = sale.Date;
			}

			//is the customer active? it is if it bought something the in the last year or if it has at least one active subscription
			if (LastSaleDate != null && DateTime.Today.Subtract(LastSaleDate.Value).TotalDays > 365 && !SoldProducts.Any())
			{
				Active = false;
			}
			else
			{
				Active = true;
			}
		}

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
		public void Merge(Customer customer)
		{
			if (customer.Id == this.Id)
			{
				throw new ArgumentException("Can't merge the same customer", "customer");
			}

			foreach (Sale s in customer.Sales)
			{
				s.Customer = this;
				s.Update();
			}

			foreach (CompanyContact s in customer.Contacts)
			{
				s.Company = this;
				s.Update();
			}

			foreach (CompanyAddress s in customer.Locations)
			{
				s.Company = this;
				s.Update();
			}

			foreach (ProductInstance s in customer.SoldProducts)
			{
				s.SoldTo = this;
				s.Update();
			}

			//delete the other customer
			customer.Delete();

			this.Save();
		}
	}
}