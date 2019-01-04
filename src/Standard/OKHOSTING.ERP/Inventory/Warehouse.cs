using OKHOSTING.ERP.New.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Inventory
{
	public class Warehouse : CompanyAddress
	{
		public string Name { get; set; }

		public ICollection<WarehouseInventory> Inventory
		{
			get;
			set;
		}

		public ICollection<WarehouseTransaction> Transactions
		{
			get;
			set;
		}
	}
}