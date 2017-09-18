using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM;
using OKHOSTING.ERP.Vendors;
using static OKHOSTING.ORM.Model.PersistentObjectExtensions;
using System;
using OKHOSTING.ERP.Production;

namespace OKHOSTING.ERP.ORM
{
	public static class VendorExtensions
	{
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
		public static void Merge(this Vendor thisVendor, Vendor vendor)
		{
			if (vendor.Id == thisVendor.Id)
			{
				throw new ArgumentException("Can't merge the same vendor", "vendor");
			}

			foreach (Purchase s in vendor.Purchases)
			{
				s.Vendor = thisVendor;
				s.Update();
			}

			foreach (CompanyContact s in vendor.Contacts)
			{
				s.Company = thisVendor;
				s.Update();
			}

			foreach (CompanyAddress s in vendor.Locations)
			{
				s.Company = thisVendor;
				s.Update();
			}

			foreach (ProductInstance s in vendor.PurchasedProducts)
			{
				s.PurchasedTo = thisVendor;
				s.Update();
			}

			//delete the other customer
			vendor.Delete();

			thisVendor.Save();
		}

		/// <summary>
		/// Deletes all invoices of this vendor
		/// </summary>
		public static void OnBeforeDelete(this Vendor vendor, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeDelete(sender, eventArgs);
			
			foreach (var s in vendor.Purchases)
			{
				sender.Delete(s);
			}
		}
	}
}