using System;
using System.Linq;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using OKHOSTING.ERP.New.Production;


using OKHOSTING.ERP.New.HR;

namespace OKHOSTING.ERP.New.Vendors
{
	/// <summary>
	/// A vendor who sales products or services to the company
	/// </summary>
	public class Vendor : Company
	{
		public VendorCategory Category
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

		/// <summary>
		/// Total balance of the customer so far, including taxes
		/// </summary>
		public decimal Balance
		{
			get;
			set;
		}

		public ICollection<Purchase> Purchases
		{
			get;
			set;
		}

		public ICollection<Production.ProductInstance> PurchasedProducts
		{
			get;
			set;
		}

		/// <summary>
		/// Calculates current vendor's balance
		/// </summary>
		public void CalculateBalance()
		{
			Balance = 0;

			foreach (Purchase purchase in Purchases)
			{
				Balance += purchase.Balance;
			}
		}
	}
}