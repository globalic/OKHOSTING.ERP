using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OKHOSTING.ERP.New.HR;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Customers
{
	/// <summary>
	/// Represents a customer feedback where the customer is expressing his overall satisfaction level with our service
	/// </summary>
	public class CustomerFeedback
	{
		public Guid Id { get; set; }

		public short SatisfactionLevel
		{
			get;
			set;
		}

		[RequiredValidator]
		public Customer Customer
		{
			get;
			set;
		}

		[RequiredValidator]
		public DateTime Date
		{
			get;
			set;
		}

		public Team Team
		{
			get;
			set;
		}

		public Employee Employee
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Comment
		{
			get;
			set;
		}
	}
}