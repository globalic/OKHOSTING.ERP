using OKHOSTING.ERP.Customers;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.ORM
{
	public static class SaleExtensions
	{
		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		protected static void OnAfterInsert(this Sale sale, DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate customer balance
			sale.Customer.SelectOnce();
			sale.Customer.CalculateStatistics();
			sale.Customer.Save();
		}

		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		protected override void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterUpdate(sender, eventArgs);

			//re-calculate customer balance
			Customer.SelectOnce();
			Customer.CalculateStatistics();
			Customer.Save();
		}

		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		protected override void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnAfterDelete(sender, eventArgs);

			//re-calculate customer balance
			Customer.SelectOnce();
			Customer.CalculateStatistics();
			Customer.Save();
		}

	}
}