using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OKHOSTING.ERP.New.Accounting
{
	/// <summary>
	/// A calculated account that takes it's value from evaluating an expression on a specific object
	/// </summary>
	public class ObjectCalculatedAccount
	{
		/// <summary>
		/// object that will be evaluated
		/// </summary>
		/// <example>
		/// OKHOSTING.ERP.New.Customers.Customer, Oid={AD2451-64122F-78128A-E24516}
		/// </example>
		//public XPWeakReference EvaluatedObject
		//{
		//	get { return GetPropertyValue<XPWeakReference>("EvaluatedObject"); }
		//	set { SetPropertyValue("EvaluatedObject", value); }
		//}

		/// <summary>
		/// Evaluation expression to automatically get the upadted value of this account
		/// </summary>
		/// <example>
		/// TotalSold
		/// Sales.Sum(Balance)
		/// </example>
		//public string EvaluationExpression
		//{
  //		  get;
  //		  set;
		//}

		/// <summary>
		/// Calculates the account's current value
		/// </summary>
		/// <returns>
		/// A decimal containing the value that will be assigned to value property
		/// </returns>
		//public override decimal CalculateCurrentValue()
		//{
		//	return Convert.ToDecimal(((XPBaseObject)EvaluatedObject.Target).Evaluate(EvaluationExpression));
		//}
	}
}