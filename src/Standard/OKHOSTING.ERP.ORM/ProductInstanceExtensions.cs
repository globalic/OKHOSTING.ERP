using OKHOSTING.ERP.New;
using OKHOSTING.ERP.New.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM;
using OKHOSTING.ERP.New.Customers;
using OKHOSTING.ERP.New.Vendors;

namespace OKHOSTING.ERP.ORM
{
	public static class ProductInstanceExtensions
	{
		/// <summary>
		/// Every time an InvoiceItem is inserted (wich means a Sale or a Purchase was made)
		/// we check Invoiceitem.Product.ProductInstanceType, and if it is not null, we create an
		/// ProductInstance of that type and save it to the Database. 
		/// InvoiceItem.Description is copied to ProductInstance.Name
		/// </summary>
		/// <remarks>
		/// Use this to control inventory merchandise, rented services or goods
		/// domain names or hosting packages. 
		/// Each ProductInstance represents the instance of a product or service, and
		/// has a Unique Id (Name) for a Product. Name can be the serial number of a product
		/// or a unique identifier like a domain name or a username
		/// </remarks>
		public static void InvoiceItem_AfterInsert(this InvoiceItem item)
		{
			//if ProductInstanceType is null, exit
			if (item.Product.ProductInstanceType == null) return;

			//select from database, in case this instance already exist
			using (var db = DataBase.CreateDataBase())
			{
				var select = new Select<ProductInstance>();
				var dtype = DataType<ProductInstance>.GetMap();
				var productType = DataType<Product>.GetMap();

				select.AddMembers
				(
					m => m.Id,
					m => m.Name,
					m => m.Product,
					m => m.Product.ProductInstanceType
				);

				select.Where.Add(new OKHOSTING.ORM.Filters.ValueCompareFilter() { Member = dtype[dm => dm.Name], ValueToCompare = item.Description });
				select.Where.Add(new OKHOSTING.ORM.Filters.ValueCompareFilter() { Member = productType[dm => dm.ProductInstanceType], ValueToCompare = item.Product.ProductInstanceType, TypeAlias = select.Joins.First().Alias });

				var instances = db.Select(select);

				ProductInstance instance = null;

				if (instances.Any())
				{
					instance = instances.First();
				}
				else
				{
					//create a new instance
					instance = new ProductInstance();
					instance.Name = item.Description;
					instance.Instance = Core.BaitAndSwitch.Create(item.Product.ProductInstanceType);
				}

				instance.Product = item.Product;
				item.ProductInstance = instance;

				//assing customer if this was a sale, or vendor if it was a purchase
				if (item.Invoice is Sale)
				{
					instance.SoldTo = ((Sale) item.Invoice).Customer;
				}
				else if (item.Invoice is Purchase)
				{
					instance.PurchasedTo = ((Purchase) item.Invoice).Vendor;
				}

				//let the instance do it's custom job
				//instance.OnInvoiceItem_AfterInsert(item);

				//save
				db.Save(instance);
				db.Save(item);
			}
		}
	}
}
