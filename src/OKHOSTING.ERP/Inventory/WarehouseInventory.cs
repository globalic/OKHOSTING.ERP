using System;

namespace OKHOSTING.ERP.Inventory
{
	public class WarehouseInventory : ORM.Model.Base<Guid>
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