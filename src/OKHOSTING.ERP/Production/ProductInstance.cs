using System;
using OKHOSTING.ERP.Vendors;
using OKHOSTING.ERP.Customers;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Production
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
		/// <summary>
		/// A unique Id like the serial number of the product, the name of a domain name
		/// or a 3rd party's product Id
		/// Combination os Product and AuxId must be unique
		/// </summary>
		[RequiredValidator]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// The DataType of this object
		/// </summary>
		[RequiredValidator]
		public Type Type
		{
			get 
			{ 
				return this.GetType(); 
			}
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

		/// <summary>
		/// Child classes should override this method to perform
		/// custom operations when a ProductInstance is created or modified
		/// by a new InvoiceItem
		/// </summary>
		/// <param name="item">InvoiceItem that was created and that affects this product instance</param>
		protected virtual void OnInvoiceItem_AfterInsert(InvoiceItem item)
		{
			//do nothing here, child classes can override this
		}

		//Static

		/// <summary>
		/// Subscribes to database events to create a product instances every time an InvoiceItem
		/// is inserted if ProductinstanceType != null
		/// </summary>
		public static void PlugIn_OnSessionStart()
		{
			//DataType dtype = typeof(InvoiceItem);

			//dtype.AfterInsert += new DataType.OperationEventHandler(InvoiceItem_AfterInsert);
		}

		/*
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
			//if this is a quote, exit
			if (item.Invoice.InvoiceType == InvoiceType.Quote) return;

			//if ProductInstanceType is null, exit
			if (item.Product.ProductInstanceType == null) return;

			//select from database, in case this instance already exist
			ICollection instances = new ICollection(PersistentCriteriaEvaluationBehavior.InTransaction, item.Session, item.Session.Dictionary.GetClassInfo(item.Product.ProductInstanceType), new BinaryOperator("Name", item.Description), true); 
			instances.Sorting.Add(new SortProperty("Items.Count", DevExpress.Xpo.DB.SortingDirection.Descending));
			
			ProductInstance instance = null;

			if(instances.Count > 0)
			{
				instance = (ProductInstance) instances[0];
				
				if (instance.IsDeleted)
				{
					instance.Purge();
					instance = null;
				}
			}
			
			if (instance == null)
			{
				//look for CreateFromInvoiceItem static method
				MethodInfo method = item.Product.ProductInstanceType.GetMethod("CreateFromInvoiceItem");

				if (method != null && method.IsStatic && method.ReturnParameter != null)
				{
					instance = (ProductInstance) method.Invoke(null, new object[] { item });
				}
				//else create new object from zero
				else
				{
					instance = (ProductInstance)item.Product.ProductInstanceType.CreateInstance(item.Session);
					instance.Name = item.Description;
				}
			}

			instance.Product = item.Product;
			item.ProductInstance = instance;

			//assing customer if this was a sale, or vendor if it was a purchase
			if (item.Invoice is Sale)
			{
				instance.SoldTo = ((Sale)item.Invoice).Customer;
			}
			else if (item.Invoice is Purchase)
			{
				instance.PurchasedTo = ((Purchase)item.Invoice).Vendor;
			}

			//let the instance do it's custom job
			instance.OnInvoiceItem_AfterInsert(item);

			//save
			instance.Save();
			item.Save();
		}

		/// <summary>
		/// Synchronizes all the product instances with in your DataBase with
		/// a list product instances imported from an external source. use this methods to
		/// synchronize your DataBase with an external source of product instances,
		/// pe: an enom account or a plesk server.
		/// </summary>
		/// <param name="externalList">
		/// A list of product instances comming from an external source, usually a 3rd party API or database
		/// </param>
		/// <param name="productInstanceType">
		/// Type of ProductInstance that will be synchronized
		/// </param>
		/// <remarks>
		/// Deletes all instances in your DataBase that does not exist in your external source anymore
		/// </remarks>
		public static void Synchronize(List<ProductInstance> externalList, Type productInstanceType)
		{
			//logging
			const string LogType = "OKHOSTING.ERP.Production.ProductInstance.Synchronize()";
			const string LogSource = "Synchronize()";

			if (externalList == null) throw new ArgumentNullException("externalList");

			#region Deleting instances in the database that no longer exists in the external list

			//Loading instances registered in database
			ICollection dataBaseList = new ICollection(MyApplication.XpoSession, productInstanceType);

			//Crossing instances registered on the database
			for (int i = 0; i < dataBaseList.Count; i++)
			{
				ProductInstance dbInstance = (ProductInstance) dataBaseList[i];

				//Initializing verification flag
				bool instanceExists = false;

				//Crossing the external list
				foreach (ProductInstance externalInstance in externalList)
				{
					//Validating if the instance was located
					if (externalInstance.Name == dbInstance.Name && externalInstance.Type == dbInstance.Type)
					{
						instanceExists = true;
						break;
					}
				}

				//Validating if the database instance exists on the external list
				if (!instanceExists)
				{
					//Deleting instance from database
					dbInstance.Delete();
					i--;
				}
			}
			
			#endregion

			#region Updating database with info from the external list

			//Creating filter to search for invoice items related to each product
			List<CriteriaOperator> productOrFilter_Filters = new List<CriteriaOperator>();
			
			//search for all type parents
			Type type = productInstanceType;
			while
			(
				type != typeof(OKBaseObject) &&
				type != typeof(XPBaseObject) &&
				type != typeof(object) &&
				type != null
			)
			{
				productOrFilter_Filters.Add(new BinaryOperator("Product.ProductInstanceType", type));
				type = type.BaseType;
			}

			CriteriaOperator productOrFilter = CriteriaOperator.Or(productOrFilter_Filters);

			//Searching every instance for related invoice items
			foreach (ProductInstance externalInstance in externalList)
			{
				try
				{
					//search invoice items realted to this productInstance
					var items = new ICollection<InvoiceItem>(MyApplication.XpoSession, CriteriaOperator.And(productOrFilter, new BinaryOperator("Description", externalInstance.Name)));
					items.Sorting.Add(new SortProperty("Invoice.Date", DevExpress.Xpo.DB.SortingDirection.Ascending));
					
					//Crossing the invoice items for the current domain
					foreach (InvoiceItem item in items)
					{
						externalInstance.Product = item.Product;

						//Validating if the invoice is a sale
						if (item.Invoice is Sale)
						{
							//Loading the sale
							Sale sale = (Sale)item.Invoice;

							//Establishing the costumer for current domain
							externalInstance.SoldTo = sale.Customer;
							item.ProductInstance = externalInstance;
						}
						else
						{
							item.ProductInstance = null;
						}

						//Updating current InvoiceItem associated ProductInstance
						item.Save();
					}

					//Save changes 
					externalInstance.Save();
				}
				catch (Exception e)
				{
					Log.Write(LogSource, string.Format("Instance {0} was not synchronized. Error: {1}\n\n\n", externalInstance.Name, e), LogType);
				}
			}

			#endregion

			Log.Write(LogSource, "End synchronization of external list of type " + productInstanceType.FullName, LogType);
		}


		/// <summary>
		/// Synchronizes all the InvoiceItems with the associated Product and creates all ProductInstances that should have been created when the InvoiceItems where inserted
		/// </summary>
		/// <param name="product">
		/// A product for which you want to create related ProductInstances that should have been generated when the InvoiceItems where inserted,
		/// but for some reason the where not created
		/// </param>
		[Action]
		public static void Synchronize(Product product)
		{
			const string LogType = "OKHOSTING.ERP.Production.ProductInstance.Synchronize(OKHOSTING.ERP.Production.Product)";
			const string LogSource = "Synchronize(OKHOSTING.ERP.Production.Product)";

			if (product == null) throw new ArgumentNullException("product");

			Log.Write(LogSource, "Begin synchronization of product " + product.GetType().FullName, LogType);
			
			//load all InvoiceItems that have this product assigned
			using (ICollection<InvoiceItem> invoiceItems = new ICollection<InvoiceItem>(product.Session, new BinaryOperator("Product", product), new SortProperty("Invoice.Date", DevExpress.Xpo.DB.SortingDirection.Ascending)))
			{
				//rise insert event for every item, like it's being inserted right now so a Productinstance is created or updated
				for (int i= 0; i < invoiceItems.Count; i++)
				{
					InvoiceItem item = invoiceItems[i];
					try
					{
						//re-create Productinstance
						DataBaseOperationEventArgs args = new DataBaseOperationEventArgs(DataBaseOperation.Insert, item);
						//ProductInstance.InvoiceItem_AfterInsert(item);
					}
					catch (Exception e)
					{
						Log.Write(LogSource, e.ToString(), LogType);
					}
				}
			}

			Log.Write(LogSource, "End synchronization of product " + product.GetType().FullName, LogType);
		}*/
	}
}