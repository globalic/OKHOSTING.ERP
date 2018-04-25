using System;
using System.Collections.Generic;

namespace OKHOSTING.ERP.New.Production
{
	/// <summary>
	/// A product that is a "package" of products, all included products are sold or purchased by a fixed price
	/// </summary>
	public class PackageProduct : Product
	{
		public ICollection<PackageProductIncludedProduct> IncludedProducts
		{
			get;
			set;
		}

		/// <summary>
		/// Monitor all inserted invoices, and if an invoice contains a PackageProduct in one of it's InvoiceItems,
		/// This methods adds all products included in the PackageProduct to the invoice
		/// </summary>
		public void InvoiceItem_BeforeInsert(InvoiceItem item)
		{
			//if this is a package product, proceed
			if (item.Product is PackageProduct)
			{
				PackageProduct packageProduct = (PackageProduct)item.Product;

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