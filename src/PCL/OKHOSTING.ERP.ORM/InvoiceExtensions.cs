using OKHOSTING.ERP.New;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.ORM
{
	public static class InvoiceExtensions
	{

		/// <summary>
		/// Deletes all items and payments of this invoice
		/// <para xml:lang="es">
		/// Elimina todos los articulos y los pagos en esta factura
		/// </para>
		/// </summary>
		public static void OnBeforeDelete(this Invoice invoice, DataBase sender, OperationEventArgs eventArgs)
		{
			foreach (var i in invoice.Items)
			{
				sender.Delete(i);
			}

			foreach (var p in invoice.Payments)
			{
				sender.Delete(p);
			}
		}

		/*
		/// <summary>
		/// Search for list and subscription items and deal with them
		/// <para xml:lang="es">
		/// Busca lista y articulos suscritos para manejarlos
		/// </para>
		/// </summary>
		protected override void  OnBeforeInsert()
		{
			base.OnBeforeInsert();

			//if no items are defined, exit
			if (Items == null) return;

			//search for subscription products
			for (int i = 0; i < Items.Count; i++)
			{
				InvoiceItem item = (InvoiceItem)Items[i];
				SubscriptionProduct subscriptionProduct = new SubscriptionProduct();
				subscriptionProduct.Id = item.Product.Id;

				if (DataBase.Current.Select(subscriptionProduct) != null)
				{
					item.Product = subscriptionProduct;

					//for subscriptions, create a SubscriptionInvoiceItem
					SubscriptionInvoiceItem subscriptionItem = new SubscriptionInvoiceItem();

					//copy values and replace current item
					item.CopyTo(subscriptionItem);
					subscriptionItem.Taxes = item.Taxes;
					foreach (InvoiceItemTax tax in subscriptionItem.Taxes)
					{
						tax.Item = subscriptionItem;
					}

					Items[i] = subscriptionItem;
				}
			}
		}
		*/

		/// <summary>
		/// Inserts all items and payments along with the current invoice
		/// <para xml:lang="es">
		/// Inserta todos los articulos y pagos en la factura actual 
		/// </para>
		/// </summary>
		public static void OnAfterInsert(this Invoice invoice, DataBase sender, OperationEventArgs eventArgs)
		{
			foreach (var i in invoice.Items)
			{
				sender.Save(i);
			}

			foreach (var p in invoice.Payments)
			{
				sender.Save(p);
			}
		}
	}
}