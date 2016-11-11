using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	public class WarehouseTransaction
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
		public Product Product
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

		[StringLengthValidator(100)]
		public string Reason
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