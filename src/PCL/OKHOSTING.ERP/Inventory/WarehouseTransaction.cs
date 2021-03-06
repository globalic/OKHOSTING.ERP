﻿using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Inventory
{
	public class WarehouseTransaction
	{
		public Guid Id { get; set; }

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

		public Production.Task Task
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