using System.Collections.Generic;

namespace OKHOSTING.ERP.New.Inventory
{
	public class WarehouseProduct : Production.Product
	{
		public decimal ReorderPoint
		{
			get;
			set;
		}

		public string Unit
		{
			get;
			set;
		}

		public ICollection<WarehouseInventory> Inventory
		{
			get;
			set;
		}
	}
}