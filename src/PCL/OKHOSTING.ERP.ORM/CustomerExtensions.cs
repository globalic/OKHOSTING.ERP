using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ERP.Customers;
using OKHOSTING.ERP.Production;
using static OKHOSTING.ORM.Model.PersistentObjectExtensions;
using System;

namespace OKHOSTING.ERP.ORM
{
	public static class CustomerExtensions
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
		/// <param name="customer">Customer that willl be merged and deleted</param>
		public static void Merge(this Customer thisCustomer, Customer customer)
		{
			if (customer.Id == thisCustomer.Id)
			{
				throw new ArgumentException("Can't merge the same customer", "customer");
			}

			foreach (Sale s in customer.Sales)
			{
				s.Customer = thisCustomer;
				s.Update();
			}

			foreach (CompanyContact s in customer.Contacts)
			{
				s.Company = thisCustomer;
				s.Update();
			}

			foreach (CompanyAddress s in customer.Locations)
			{
				s.Company = thisCustomer;
				s.Update();
			}

			foreach (ProductInstance s in customer.SoldProducts)
			{
				s.SoldTo = thisCustomer;
				s.Update();
			}

			//delete the other customer
			customer.Delete();

			thisCustomer.Save();
		}

		/// <summary>
		/// Deletes all Sales of this customer
		/// </summary>
		public static void OnBeforeDelete(this Customer customer, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeDelete(sender, eventArgs);

			foreach (var s in customer.Sales)
			{
				s.Delete();
			}
		}
	}
}