using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Inventory
{
	public class WarehouseTransaction : ORM.PersistentClass<Guid>
	{
		[RequiredValidator]
		public DateTime Date
		{
			get;
			set;
		}

		[RequiredValidator]
		public Warehouse Warehouse
		{
			get;
			set;
		}

		[RequiredValidator]
		public WarehouseProduct Product
		{
			get;
			set;
		}

		[RequiredValidator]
		public decimal Quantity
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Notes
		{
			get;
			set;
		}

		[RequiredValidator]
		public HR.Employee Supervisor
		{
			get;
			set;
		}

		public Production.Project Project
		{
			get;
			set;
		}

		public Invoice Invoice
		{
			get;
			set;
		}	
	}
}