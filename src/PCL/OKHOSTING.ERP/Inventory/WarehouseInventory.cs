using System;

namespace OKHOSTING.ERP.New.Inventory
{
	public class WarehouseInventory
	{
		public Guid Id { get; set; }

		public WarehouseProduct Product
		{
			get;
			set;
		}

		public Warehouse Warehouse
		{
			get;
			set;
		}

		public decimal Existance
		{
			get;
			set;
		}
	}
}