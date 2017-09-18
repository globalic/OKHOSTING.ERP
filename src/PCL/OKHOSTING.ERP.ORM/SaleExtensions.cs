using OKHOSTING.ERP.Customers;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM.Model;

namespace OKHOSTING.ERP.ORM
{
	public static class SaleExtensions
	{
		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		public static void OnAfterInsert(this Sale sale, DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate customer balance
			sale.Customer.Select();
			sale.Customer.CalculateStatistics();
			sale.Customer.Save();
		}

		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		public static void OnAfterUpdate(this Sale sale, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			//re-calculate customer balance
			sale.Customer.Select();
			sale.Customer.CalculateStatistics();
			sale.Customer.Save();
		}

		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		public static void OnAfterDelete(this Sale sale, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterDelete(sender, eventArgs);

			//re-calculate customer balance
			sale.Customer.Select();
			sale.Customer.CalculateStatistics();
			sale.Customer.Save();
		}
	}
}