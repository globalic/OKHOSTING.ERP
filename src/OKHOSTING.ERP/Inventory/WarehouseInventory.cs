using System;

namespace OKHOSTING.ERP.Inventory
{
	public class WarehouseInventory : ORM.PersistentClass<Guid>
	{
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