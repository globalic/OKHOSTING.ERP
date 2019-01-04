using System;
using OKHOSTING.ERP.New.Finances;
using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// Tax applied to an invoice item
	/// <para xml:lang="es">
	/// Impuesto aplicado a un articulo facturado
	/// </para>
	/// </summary>
	public class InvoiceItemTax
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Invoice item that generated this tax
		/// <para xml:lang="es">
		/// Articulo facturado que genera este impuesto
		/// </para>
		/// </summary>
		[RequiredValidator]
		public InvoiceItem Item
		{
			get;
			set;
		}

		/// <summary>
		/// Tax being applied to the invoice item
		/// <para xml:lang="es">
		/// Impuesto que esta siendo aplicado al articulo facturado
		/// </para>
		/// </summary>
		[RequiredValidator]
		public Tax Tax
		{
			get;
			set;
		}

		/// <summary>
		/// Ammount being charged as tax
		/// <para xml:lang="es">
		/// Cantidad siendo cargada como un impuesto
		/// </para>
		/// </summary>
		public decimal? Amount
		{
			get;
			set;
		}

		/// <summary>
		/// Calculates the amount of the tax
		/// <para xml:lang="es">
		/// Calcula la cantidad del impuesto
		/// </para>
		/// </summary>
		public void CalculateAmount()
		{
			//Tax?.SelectOnce();
			//Item?.SelectOnce();
			Amount = Tax?.GetTaxFor(Item.Subtotal);
		}

		/// <summary>
		/// Calculates the amount of tax
		/// <para xml:lang="es">
		/// Calcula la cantidad del impuesto
		/// </para>
		/// </summary>
		public void OnBeforeInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeInsert(sender, eventArgs);

			CalculateAmount();
		}

		/// <summary>
		/// Calculates the amount of tax
		/// <para xml:lang="es">
		/// Calcula la cantidad del impuesto
		/// </para>
		/// </summary>
		public void OnBeforeUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeUpdate(sender, eventArgs);

			CalculateAmount();
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// <para xml:lang="es">
		/// Recalcula los totales de los articulos facturados
		/// </para>
		/// </summary>
		public void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			//re-calculate invoice item totals
			sender.Select(Item);
			Item.CalculateTotals();
			sender.Update(Item);
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// <para xml:lang="es">
		/// Recalcula los totales de los articulos facturados
		/// </para>
		/// </summary>
		public void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate invoice item totals
			sender.Select(Item);
			Item.CalculateTotals();
			sender.Update(Item);
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// <para xml:lang="es">
		/// Recalcula los totales de los articulos facturados
		/// </para>
		/// </summary>
		public void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate invoice item totals
			sender.Select(Item);
			Item.CalculateTotals();
			sender.Update(Item);
		}
	}
}