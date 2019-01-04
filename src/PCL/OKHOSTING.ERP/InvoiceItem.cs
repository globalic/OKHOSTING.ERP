using System;
using System.Linq;
using OKHOSTING.ERP.New.Production;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// An item that belongs to an invoice
	/// <para xml:lang="es">
	/// Un item que pertenece a una factura
	/// </para>
	/// </summary>
	public class InvoiceItem
	{
		public Guid Id { get; set; }

		Product _Product;

		[RequiredValidator]
		public Product Product
		{
			get
			{
				return _Product;
			}
			set
			{
				_Product = value;
				
				if (value != null)
				{
					if (Price == 0)
					{
						Price = value.Price;
					}
				}
			}
		}

		/// <summary>
		/// Number of items sold
		/// <para xml:lang="es">
		/// Numero de articulos vendidos
		/// </para>
		/// </summary>
		[RequiredValidator]
		public decimal Quantity
		{
			get;
			set;
		}

		/// <summary>
		/// Individual product price
		/// <para xml:lang="es">
		/// Precio individual del producto
		/// </para>
		/// </summary>
		public decimal Price
		{
			get;
			set;
		}

		/// <summary>
		/// Individual product discount
		/// <para xml:lang="es">
		/// Descuento individual del producto
		/// </para>
		/// </summary>
		public decimal Discount
		{
			get;
			set;
		}

		/// <summary>
		/// Description for this item
		/// <para xml:lang="es">
		/// Descripcion del articulo
		/// </para>
		/// </summary>
		[StringLengthValidator(100)]
		[RequiredValidator]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Invoice which this item belongs to
		/// <para xml:lang="es">
		/// Factura a la que el articulo pertenece
		/// </para>
		/// </summary>
		[RequiredValidator]
		public Invoice Invoice
		{
			get;
			set;
		}

		/// <summary>
		/// Product instance that is being sold/bought in the current Invoice (optional)
		/// <para xml:lang="es">
		/// Instancia del producto que esta siendo vendido/comprado en la factura actual (opcional)
		/// </para>
		/// </summary>
		public ProductInstance ProductInstance
		{
			get;
			set;
		}

		#region Calculated fields

		/// <summary>
		/// Total sum of sales items, including discounts but not taxes
		/// </summary>
		public decimal Subtotal
		{
			get;
			set;
		}

		/// <summary>
		/// Total ammount of taxes
		/// </summary>
		public decimal Tax
		{
			get;
			set;
		}

		/// <summary>
		/// Total ammount of the sale, including discounts and taxes
		/// </summary>
		public decimal Total
		{
			get;
			set;
		}

		#endregion

		public ICollection<InvoiceItemTax> Taxes
		{
			get;
			set;
		}

		/// <summary>
		/// Total ammount of the sale, including discounts but not taxes
		/// <para xml:lang="es">
		/// Cantidad total de la venta, incluyendo descuentos pero no los impuestos
		/// </para>
		/// </summary>
		/// <value>Quantity * Price
		/// <para xml:lang="es">
		/// Cantidad * Precio
		/// </para>
		/// </value>
		private void CalculateSubtotal()
		{
			Subtotal = (this.Quantity * this.Price) - this.Discount;
		}

		/// <summary>
		/// Total ammount of taxes
		/// <para xml:lang="es">
		/// Cantidad total de impuestos
		/// </para>
		/// </summary>
		private void CalculateTax()
		{
			Tax = 0;

			foreach (InvoiceItemTax tax in this.Taxes)
			{
				tax.CalculateAmount();
				Tax += tax.Amount.Value;
			}
		}

		/// <summary>
		/// Total ammount of the sale, including taxes and discount
		/// <para xml:lang="es">
		/// Cantidad total de la venta, incluyendo impuestos y descuentos
		/// </para>
		/// </summary>
		/// <value>PriceTotal + TaxTotal
		/// <para xml:lang="es">
		/// PriceTotal + TaxTotal
		/// </para>
		/// </value>
		private void CalculateTotal()
		{
			Total = Subtotal + Tax;
		}

		/// <summary>
		/// Performs calculations for subtotal, tax, total, paid and balance
		/// <para xml:lang="es">
		/// Realiza los calculos del subtotal, impuesto, toal, pagado y balance
		/// </para>
		/// </summary>
		public void CalculateTotals()
		{
			CalculateSubtotal();
			CalculateTax();
			CalculateTotal();
		}

		public override string ToString()
		{
			return Description;
		}

		/// <summary>
		/// Deletes all taxes of this item
		/// <para xml:lang="es">
		/// Elimina todos los impuestos para este articulo
		/// </para>
		/// </summary>
		public void OnBeforeDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeDelete(sender, eventArgs);

			foreach (var tax in Taxes)
			{
				sender.Delete(tax);
			}
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// <para xml:lang="es">
		/// Recalcula los totales de las facturas
		/// </para>
		/// </summary>
		public void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate invoice totals
			sender.Select(Invoice);
			Invoice.CalculateTotals();
			sender.Update(Invoice);
		}

		/// <summary>
		/// Calculates totals
		/// <para xml:lang="es">
		/// Calcula los totales
		/// </para>
		/// </summary>
		public void OnBeforeInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//get price from product
			if (Price == 0)
			{
				Price = Product.Price;
			}

			//base.OnBeforeInsert(sender, eventArgs);
		}

		/// <summary>
		/// Inserts all items taxes along with the current item and recalculates item's and invoice's totals
		/// <para xml:lang="es">
		/// Inserta todos los impuestos de los articulos correspondientes y recalcula el total de articulos y facturas
		/// </para>
		/// </summary>
		public void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			//insert item taxes according to product
			if (Taxes == null || !Taxes.Any())
			{
				Taxes = new List<InvoiceItemTax>();

				sender.Select(Product);

				ERP.New.Finances.TaxGroup group = null;

				if (Invoice is New.Customers.Sale)
				{
					group = Product.SaleTaxes;
				}
				else if (Invoice is New.Vendors.Purchase)
				{
					group = Product.SaleTaxes;
				}

				sender.Select(group);
				sender.LoadCollection(group, g => g.Taxes);

				foreach (var t in group.Taxes)
				{
					sender.Select(t.Tax);

					InvoiceItemTax itemTax = new InvoiceItemTax();
					itemTax.Item = this;
					itemTax.Tax = t.Tax;
					itemTax.CalculateAmount();

					Taxes.Add(itemTax);

					sender.Insert(itemTax);
				}
			}

			sender.Select(Invoice);
			Invoice.CalculateTotals();
			sender.Update(Invoice);
		}

		/// <summary>
		/// Re-calculates totals
		/// <para xml:lang="es">
		/// Recalcula los totales
		/// </para>
		/// </summary>
		public void OnBeforeUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeUpdate(sender, eventArgs);
			CalculateTotals();
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// <para xml:lang="es">
		/// Recalcula los totales de las facturas
		/// </para>
		/// </summary>
		public void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate invoice totals
			sender.Select(Invoice);
			Invoice.CalculateTotals();
			sender.Update(Invoice);
		}
	}
}