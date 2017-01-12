using OKHOSTING.ERP.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	public class Warehouse : ORM.PersistentClass<Guid>
	{
		[StringLengthValidator(100)]
		[RequiredValidator]
		public string Name
		{
			get;
			set;
		}

		public Employee Manager
		{
			get;
			set;
		}

		public Address Address
		{
			get;
			set;
		}

		public ICollection<WarehouseProduct> Products
		{
			get;
			set;
		}

		public ICollection<WarehouseTransaction> Transactions
		{
			get;
			set;
		}

		/*[PersistentAlias("Sum(Products.TotalValue)")]
		public decimal TotalValue
		{
			get { return (decimal)EvaluateAlias("TotalValue"); }
		}*/

	}
}