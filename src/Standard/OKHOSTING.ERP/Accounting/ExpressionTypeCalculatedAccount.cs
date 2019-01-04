using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;

namespace OKHOSTING.ERP.Accounting
{
	/// <summary>
	/// A calculated account that takes it's value from a type and an expression
	/// </summary>
	public class ExpressionTypeCalculatedAccount : TypeCalculatedAccount
	{
		public ExpressionTypeCalculatedAccount(): base(MyApplication.XpoSession)
		{
		}

		public ExpressionTypeCalculatedAccount(DevExpress.Xpo.Session session): base(session)
		{
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
			get { return GetPropertyValue<string>("EvaluationExpression"); }
			set { SetPropertyValue("EvaluationExpression", value); }
		}

		/// <summary>
		/// Calculates the account's current value
		/// </summary>
		/// <returns>
		/// A decimal containing the value that will be assigned to value property
		/// </returns>
		public override decimal CalculateCurrentValue()
		{
			return Convert.ToDecimal(Session.Evaluate(EvaluatedType, CriteriaOperator.Parse(EvaluationExpression), CriteriaOperator.Parse(EvaluationFilter)));
		}
	}
}