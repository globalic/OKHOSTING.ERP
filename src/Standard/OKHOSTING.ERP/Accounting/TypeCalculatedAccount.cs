using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Accounting
{
	/// <summary>
	/// A calculated account that takes it's value from looking into a list of objects and evaluating an expression on it
	/// </summary>
	public class TypeCalculatedAccount : CalculatedAccount
	{
		/// <summary>
		/// Collection of objects that will be evaluated
		/// </summary>
		/// <example>
		/// OKHOSTING.ERP.New.Customers.Sale
		/// </example>
		//[ValueConverter(typeof(OKHOSTING.ValueConverters.TypeValueConverter))]
		public Type EvaluatedType
		{
			get;
			set;
		}

		/// <summary>
		/// A filter expression. Only objects that match this filter will be considered for the value evaluation
		/// </summary>
		/// <example>
		/// Date > 1/1/2013 && Date < 1/2/2013
		/// </example>
		[StringLengthValidator(200)]
		public string EvaluationFilter
		{
			get;
			set;
		}

		/// <summary>
		/// Evaluation expression to automatically get the upadted value of this account
		/// </summary>
		/// <example>
		/// Sum(Total)
		/// SubAccounts.Avg(Value)
		/// </example>
		public string EvaluationExpression
		{
			get;
			set;
		}

		/// <summary>
		/// Calculates the account's current value
		/// </summary>
		/// <returns>
		/// A decimal containing the value that will be assigned to value property
		/// </returns>
		public override decimal CalculateCurrentValue()
		{
			//	return Convert.ToDecimal(Session.Evaluate(EvaluatedType, CriteriaOperator.Parse(EvaluationExpression), CriteriaOperator.Parse(EvaluationFilter)));
			return 0;
		}
	}
}