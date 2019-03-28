using System;
using OKHOSTING.ERP.New.Vendors;
using OKHOSTING.ERP.New.Customers;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using System.Linq;

namespace OKHOSTING.ERP.New.Production
{
	/// <summary>
	/// Instance of a product, a product that was bought or sold and
	/// has unique characteristics like a serial number
	/// </summary>
	/// <remarks>
	/// Use this class as a base class for inventory products, subscriptions services that have
	/// unique characteristics and need to be tracked after or before the sale
	/// </remarks>
	/// <example>
	/// Create a class named "Television" with a property named SerialNumber 
	/// to track inventory after bought and guarantee after sold
	/// </example>
	public class ProductInstance
	{
		public Guid Id { get; set; }

		/// <summary>
		/// A unique Id like the serial number of the product, the name of a domain name
		/// or a 3rd party's product Id
		/// Combination os Product and AuxId must be unique
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(400)]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// The actual object that was sold to the customer / or bought from the vendor
		/// </summary>
		[RequiredValidator]
		public object Instance
		{
			get;
			set;
		}

		/// <summary>
		/// Product that defines this instance. Type of product that this instance is
		/// </summary>
		public Product Product
		{
			get;
			set;
		}

		/// <summary>
		/// Vendor which this product instance was purchased to (optional)
		/// </summary>
		public Vendor PurchasedTo
		{
			get;
			set;
		}

		/// <summary>
		/// Customer which this product instance was sold to (optional)
		/// </summary>
		public Customer SoldTo
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the list of invoice items that created or updated this product instance,
		/// sorted by invoice's date
		/// </summary>
		public ICollection<InvoiceItem> Items
		{
			get;
			set;
		}

		protected virtual void OnInvoiceItem_AfterInsert(InvoiceItem item)
		{
			//do nothing here, child classes can override this
		}

		//Static

		public override string ToString()
		{
			return Name;
		}

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
		public static void InvoiceItem_AfterInsert(InvoiceItem item)
		{
			//if ProductInstanceType is null, exit
			if (item?.Product?.ProductInstanceType == null)
			{
				return;
			}

			//select from database, in case this instance already exist
			using (var db = Core.BaitAndSwitch.Create<DataBase>())
			{
				IEnumerable<ProductInstance> instances = SelectFrom(item.Description, item.Product.ProductInstanceType);
				ProductInstance instance = instances.FirstOrDefault();

				//foreach (var aditional in instances.Skip(1))
				//{
				//	db.Delete(aditional);
				//}

				if (instance == null)
				{
					//search the DB for an object that  matches type and name
					Select instanceSelect = new Select();
					instanceSelect.DataType = item.Product.ProductInstanceType;
					instanceSelect.Where.Add(new ORM.Filters.ValueCompareFilter(new DataMember(item.Product.ProductInstanceType, "Name"), item.Description));
					var instanceInstance = db.SelectInherited(instanceSelect).FirstOrDefault();

					//create a new instance
					if (item.Product is SubscriptionProduct)
					{
						instance = Core.BaitAndSwitch.Create<Subscription>();
					}
					else
					{
						instance = Core.BaitAndSwitch.Create<ProductInstance>();
					}

					instance.Name = item.Description;
					instance.Instance = instanceInstance;

					//create new?
					if (instance.Instance == null)
					{
						instance.Instance = Core.BaitAndSwitch.Create(item.Product.ProductInstanceType);
						new DataMember(item.Product.ProductInstanceType, "Name").Expression.SetValue(instance.Instance, item.Description);
					}
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
				instance.OnInvoiceItem_AfterInsert(item);
			}
		}

		public static IEnumerable<ProductInstance> SelectFrom(string name, Type productInstanceType)
		{
			//select from database, in case this instance already exist
			using (var db = Core.BaitAndSwitch.Create<DataBase>())
			{
				var select = new Select<ProductInstance>();
				var dtype = DataType<ProductInstance>.GetDataType();
				var productType = DataType<Product>.GetDataType();

				select.AddMembers
				(
					m => m.Id,
					m => m.Name,
					m => m.Product,
					m => m.Product.Id,
					m => m.Product.ProductInstanceType
				);

				select.Where.Add(new ORM.Filters.ValueCompareFilter() { Member = dtype[dm => dm.Name], ValueToCompare = name });
				select.Where.Add(new ORM.Filters.ValueCompareFilter() { Member = productType[dm => dm.ProductInstanceType], ValueToCompare = productInstanceType, TypeAlias = select.Joins.First().Alias });

				return db.SelectInherited(select);
			}
		}

		public static IEnumerable<ProductInstance> SelectFrom(object instance)
		{
			//select from database, in case this instance already exist
			using (var db = Core.BaitAndSwitch.Create<DataBase>())
			{
				var select = new Select<ProductInstance>();
				var dtype = DataType<ProductInstance>.GetDataType();
				var productType = DataType<Product>.GetDataType();

				select.AddMembers(dtype.AllDataMembers);
				select.Where.Add(new ORM.Filters.ValueCompareFilter() { Member = dtype[dm => dm.Instance], ValueToCompare = instance});

				return db.SelectInherited(select);
			}
		}

		public static void DeleteWhereInstanceDoesNotExist()
		{
			//select from database, in case this instance already exist
			using (var db = Core.BaitAndSwitch.Create<DataBase>())
			using (var db2 = Core.BaitAndSwitch.Create<DataBase>())
			{
				var select = new Select<ProductInstance>();
				select.AddMembers(select.DataType.AllDataMembers);

				foreach (var instance in db.SelectInherited(select))
				{
					if (!db2.Exist(instance.Instance))
					{
						foreach (var item2 in instance.Items)
						{
							item2.ProductInstance = null;
							db2.Update(item2);
						}

						db2.Delete(instance);
					}
				}
			}
		}
	}
}