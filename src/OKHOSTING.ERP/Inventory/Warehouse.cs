using OKHOSTING.ERP.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Inventory
{
	public class Warehouse : CompanyLocation
	{
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