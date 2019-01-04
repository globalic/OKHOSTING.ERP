using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ERP.New;
using OKHOSTING.ERP.New.Customers;
using OKHOSTING.ERP.New.Production;
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
		/// <para xml:lang="es">
		/// Combina un cliente con el cliente actual. Transfuere todos los datos del otro cliente al cliente actual,
		/// incluyendo facturas, contactos, direcciones, etc.
		/// Reasigna todas las llaves foraneas relacionadas con datos the otro cliente al cliente actual
		/// y elimina al cliente combinado al final. 
		/// </para>
		/// </summary>
		/// <remarks>
		/// The merged Customer will be deleted. Customer properties will not be copied into the current Customer, only foreign-key related DataObjects will 
		/// be reasigned to the current one
		/// <para xml:lang="es">
		/// El cliente cominado será eliminado. Las propiedades del cliente no serán copiadas al actual, solo las llaves foraneas relacionadas a DataObjects serán
		/// reasignadas al actual. 
		/// </para>
		/// </remarks>
		/// <param name="customer">Customer that will be merged and deleted
		/// <para xml:lang="es">
		/// Cliente que será combinado y eliminado
		/// </para>
		///</param>
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
		/// <para xml:lang="es">
		/// Elimina todas las ventas para este cliente
		/// </para>
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