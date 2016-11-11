using System;
using System.Linq;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Vendors
{
	/// <summary>
	/// A vendor who sales products or services to the company
	/// </summary>
	public class Vendor
	{
		[RequiredValidator]
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
			get
			{
				return Purchases.Sum(p => p.Balance);
			}
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
		//public void Merge(Vendor vendor)
		//{
		//	if (vendor.Oid == this.Oid)
		//	{
		//		throw new ArgumentException("Can't merge the same vendor", "vendor");
		//	}

		//	while (vendor.Purchases.Count > 0)
		//	{
		//		Purchase s = vendor.Purchases[0];
		//		s.Vendor = this;
		//		s.Save();
		//	}

		//	while (vendor.Contacts.Count > 0)
		//	{
		//		CompanyContact s = vendor.Contacts[0];
		//		s.Company = this;
		//		s.Save();
		//	}

		//	while (vendor.Addresses.Count > 0)
		//	{
		//		CompanyAddress s = vendor.Addresses[0];
		//		s.Company = this;
		//		s.Save();
		//	}

		//	//delete the other customer
		//	vendor.Delete();

		//	Save();
		//}
	}
}