using OKHOSTING.ERP.New;
using OKHOSTING.ERP.New.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM;

namespace OKHOSTING.ERP.ORM
{
	public static class PackageProductExtensions
	{
		/// <summary>
		/// Monitor all inserted invoices, and if an invoice contains a PackageProduct in one of it's InvoiceItems,
		/// This methods adds all products included in the PackageProduct to the invoice
		/// <para xml:lang="es">
		/// Monitoera todas las facturas insertadas, si una factura contiene PackageProduct en one de sus InvoiceItems, 
		/// este metodo añado todos los productos incluídos en el paquete a la factura. 
		/// </para>
		/// </summary>
		public static void InvoiceItem_BeforeInsert(this InvoiceItem item)
		{
			//if this is a package product, proceed
			if (item.Product is PackageProduct)
			{
				PackageProduct packageProduct = (PackageProduct) item.Product;

				//list products
				InvoiceItem includedItem;

				//add all included products as items with price = 0
				foreach (PackageProductIncludedProduct includedProduct in packageProduct.IncludedProducts)
				{
					includedItem = new InvoiceItem();
					includedItem.Price = includedItem.Discount = 0;
					includedItem.Product = includedProduct.IncludedProduct;
					includedItem.Quantity = includedProduct.Quantity * item.Quantity;
					includedItem.Description = item.Description;

					item.Invoice.Items.Add(includedItem);
				}
			}
		}
	}
}
