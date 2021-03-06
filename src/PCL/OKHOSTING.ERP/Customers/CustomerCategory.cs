using System;
using OKHOSTING.Data;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.New.Customers
{
	/// <summary>
	/// A category for customers
	/// </summary>
	public class CustomerCategory
	{
		public Guid Id { get; set; }

		[StringLengthValidator(250)]
		[RequiredValidator]
		public string Name
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		[RequiredValidator]
		public decimal PriceAdjustment
		{
			get;
			set;
		}

		public ICollection<Customer> Customers
		{
			get;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}