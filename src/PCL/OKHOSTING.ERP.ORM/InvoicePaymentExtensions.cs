﻿using OKHOSTING.ERP.New;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using static OKHOSTING.ORM.Model.PersistentObjectExtensions;

namespace OKHOSTING.ERP.ORM
{
	public static class InvoicePaymentExtensions
	{

		/// <summary>
		/// Recalculates invoice's totals
		/// <para xml:lang="es">
		/// Recalcula los totales de facturas
		/// </para>
		/// </summary>
		public static void OnAfterDelete(this InvoicePayment payment, DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate invoice totals
			payment.Invoice.Select();
			payment.Invoice.CalculateTotals();
			payment.Invoice.Update();
		}

		/// <summary>
		/// Inserts all items taxes along with the current item and recalculates item's and invoice's totals
		/// <para xml:lang="es">
		/// Inserta todos los impuesos de articulos al elemento actual y recalcula los totales de articulos y facturas. 
		/// </para>
		/// </summary>
		public static void OnAfterInsert(this InvoicePayment payment, DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate invoice totals
			payment.Invoice.Select();
			payment.Invoice.CalculateTotals();
			payment.Invoice.Update();
		}

		/// <summary>
		/// Recalculates invoice's totals
		/// <para xml:lang="es">
		/// Recalcula los totales de facturas
		/// </para>
		/// </summary>
		public static void OnAfterUpdate(this InvoicePayment payment, DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate invoice totals
			payment.Invoice.Select();
			payment.Invoice.CalculateTotals();
			payment.Invoice.Update();
		}
	}
}