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
	}
}