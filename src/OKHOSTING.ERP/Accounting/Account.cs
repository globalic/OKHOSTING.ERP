using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Accounting
{
	/// <summary>
	/// An account represents anything that can be "counted" in the company. It can be the number of customers,
	/// number of sales the past month, total income of the past year, or anything that is a number and is
	/// important for decition making
	/// </summary>
	public class Account
	{
        /// <summary>
        /// Name of the account
        /// </summary>
        /// <example>
        /// Total customers, active customers, sales this month, revenue last month, expenses last year, etc.
        /// </example>
        [StringLengthValidator(100)]
        [RequiredValidator]
		public string Name
		{
            get;
            set;
		}

		/// <summary>
		/// Parent account, usefull for organization
		/// </summary>
		public Account Parent
		{
            get;
            set;
		}

        /// <summary>
        /// More detailed description of the account
        /// </summary>
        /// <example>
        /// Describes in better detail what this account is keeping track of
        /// </example>
        [StringLengthValidator(StringLengthValidator.Unlimited)]
        public string Description
		{
            get;
            set;
		}

		/// <summary>
		/// Current value of the account
		/// </summary>
		/// <example>
		/// 3000 customers, $200,000 in revenue last month, etc.
		/// </example>
		[RequiredValidator]
		public decimal Value
		{
            get;
			set;
		}

		public ICollection<Account> SubAccounts
		{
            get;
            set;
		}

		public ICollection<AccountUpdate> History
		{
			get;
			set;
		}

		public ICollection<AccountProjection> Projections
		{
			get;
			set;
		}
	}
}