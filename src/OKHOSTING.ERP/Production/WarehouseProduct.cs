using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Production
{
	public class WarehouseProduct : ORM.PersistentClass<Guid>
	{
		[RequiredValidator]
		public Warehouse Warehouse
		{
			get;
			set;
		}

		//[RuleRequiredField]
		public Product Product
		{
			get;
			set;
		}

		[RequiredValidator]
		public decimal OnHand
		{
			get;
			set;
		}

		public decimal ReorderPoint
		{
			get;
			set;
		}

		/*
		[PersistentAlias("Product.StandardCost * OnHand")]
		public decimal TotalValue
		{
			get { return (decimal) EvaluateAlias("TotalValue"); }
		}

		public static void InvoiceItem_AfterInsert(InvoiceItem item)
		{
			//if this is a quote, exit
			if (item.Invoice.InvoiceType == InvoiceType.Quote) return;

			WarehouseProduct wp = item.Session.FindObject<WarehouseProduct>(new BinaryOperator("Product", item.Product));
			ICollection<Warehouse> warehouses = new ICollection<Warehouse>(MyApplication.XpoSession);

			if (wp == null && item.Product.HasInventory)
			{
				wp = new WarehouseProduct(item.Session);

				wp.Product = item.Product;
				wp.OnHand = item.Quantity;
				wp.Warehouse = warehouses[0];

				wp.Save();
			}
			else {
				wp.OnHand = item.Quantity;
				wp.Save();
			}

			WarehouseTransaction transaction = new WarehouseTransaction(item.Session);
			transaction.Date = DateTime.Now;
			transaction.Product = item.Product;
			transaction.Quantity = item.Quantity;
			transaction.Invoice = item.Invoice;
			transaction.Warehouse = warehouses[0];

			if (item.Invoice is OKHOSTING.ERP.Customers.Sale)
			{
				transaction.Quantity = transaction.Quantity * -1;
				transaction.Reason = "Sale";
			}
			else if (item.Invoice is OKHOSTING.ERP.Vendors.Purchase)
			{
				transaction.Reason = "Purchase";
			}

			transaction.Save();

			//save
			//item.Save();
		}
*/
	}
}
 