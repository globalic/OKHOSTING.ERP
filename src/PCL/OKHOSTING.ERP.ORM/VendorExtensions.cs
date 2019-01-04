﻿using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM;
using OKHOSTING.ERP.New;
using OKHOSTING.ERP.New.Vendors;
using static OKHOSTING.ORM.Model.PersistentObjectExtensions;
using System;
using OKHOSTING.ERP.New.Production;

namespace OKHOSTING.ERP.ORM
{
	public static class VendorExtensions
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
		/// <param name="vendor">Customer that will be merged and deleted
		/// <para xml:lang="es">
		/// Cliente que será combinado y eliminado
		/// </para>
		///</param>
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
		/// <para xml:lang="es">
		/// Elimina todas las facturas de este vendedor
		/// </para>
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