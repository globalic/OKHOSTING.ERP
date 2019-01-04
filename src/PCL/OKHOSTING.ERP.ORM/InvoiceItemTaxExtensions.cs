using System;
using System.Collections.Generic;
using System.Linq;
using OKHOSTING.ERP.New;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM.Model;
using OKHOSTING.ORM;

namespace OKHOSTING.ERP.ORM
{
	public static class InvoiceItemTaxExtensions
	{

		/// <summary>
		/// Calculates the amount of tax
		/// <para xml:lang="es">
		/// Calcula el importe de impuestos
		/// </para>
		/// </summary>
		public static void OnBeforeInsert(this InvoiceItemTax itemTax, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeInsert(sender, eventArgs);

			itemTax.CalculateAmount();
		}

		/// <summary>
		/// Calculates the amount of tax
		/// <para xml:lang="es">
		/// Calcula el importe de impuestos
		/// </para>
		/// </summary>
		public static void OnBeforeUpdate(this InvoiceItemTax itemTax, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeUpdate(sender, eventArgs);

			itemTax.CalculateAmount();
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// <para xml:lang="es">
		/// Recalcula el importe total de los articulos facturados
		/// </para>
		/// </summary>
		public static void OnAfterInsert(this InvoiceItemTax itemTax, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			//re-calculate invoice item totals
			itemTax.Item.Select();
			itemTax.Item.CalculateTotals();
			itemTax.Item.Update();
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// <para xml:lang="es">
		/// Recalcula el importe total de los articulos facturados
		/// </para>
		/// </summary>
		public static void OnAfterUpdate(this InvoiceItemTax itemTax, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate invoice item totals
			itemTax.Item.Select();
			itemTax.Item.CalculateTotals();
			itemTax.Item.Update();
		}

		/// <summary>
		/// Recalculates invoice item's totals
		/// <para xml:lang="es">
		/// Recalcula el importe total de los articulos facturas
		/// </para>
		/// </summary>
		public static void OnAfterDelete(this InvoiceItemTax itemTax, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate invoice item totals
			itemTax.Item.Select();
			itemTax.Item.CalculateTotals();
			itemTax.Item.Update();
		}
	}
}
