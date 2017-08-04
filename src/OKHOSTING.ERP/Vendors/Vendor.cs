using System;
using System.Linq;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using OKHOSTING.ERP.Production;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ERP.HR;

namespace OKHOSTING.ERP.Vendors
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
		/// Merges another Customer with the current Customer. Transfers all data from the other Customer into the current one,
		/// including invoices, contacts, addresses, etc.
		/// Re-assigns all foreign-key related data from the other Customer in favor of the current one
		/// and deletes the merged Customer at the end.
		/// </summary>
		/// <remarks>
		/// The merged Customer will be deleted. Customer properties will not be copied into the current Customer, only foreign-key related DataObjects will 
		/// be reasigned to the current one
		/// </remarks>
		/// <param name="vendor">Customer that willl be merged and deleted</param>
		public void Merge(Vendor vendor)
		{
			if (vendor.Id == this.Id)
			{
				throw new ArgumentException("Can't merge the same vendor", "vendor");
			}
			
			foreach (Purchase s in vendor.Purchases)
			{
				s.Vendor = this;
				s.Update();
			}

			foreach (Employee s in vendor.Employees)
			{
				s.Company = this;
				s.Update();
			}

			foreach (CompanyLocation s in vendor.Locations)
			{
				s.Company = this;
				s.Update();
			}

			foreach (ProductInstance s in vendor.PurchasedProducts)
			{
				s.PurchasedTo = this;
				s.Update();
			}

			//delete the other customer
			vendor.Delete();

			Save();
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

		/// <summary>
		/// Deletes all invoices of this vendor
		/// </summary>
		protected override void OnBeforeDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnBeforeDelete(sender, eventArgs);

			foreach (var s in Purchases)
			{
				sender.Delete(s);
			}
		}
	}
}