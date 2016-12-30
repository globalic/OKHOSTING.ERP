using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using OKHOSTING.Tools.Extensions;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Accounting
{
	/// <summary>
	/// A calculated account that takes it's value from searching into an object list and filtering
	/// that list by a specific time frame, and then evaluating an expression on the resulting list
	/// </summary>
	/// <remarks>
	/// Usefull to keep track of numbers that are periodically counted, like monthly sales, 
	/// monthly new customers, yearly expenses, etc.
	/// </remarks>
	public class PeriodicTypeCalculatedAccount : TypeCalculatedAccount
	{
		/// <summary>
		/// The name of the DateTime property of the EvaluatedType that will be used to filter the list
		/// </summary>
		public string DateProperty
		{
			get;
			set;
		}

		/// <summary>
		/// Date when the account started
		/// </summary>
		public DateTime Start
		{
			get;
			set;
		}

		/// <summary>
		/// Lenght of the period
		/// </summary>
		[RequiredValidator]
		public int PeriodLenght
		{
			get;
			set;
		}

		/// <summary>
		/// Unit used for period lenght
		/// </summary>
		[RequiredValidator]
		public TimeUnit.Unit PeriodUnit
		{
			get;
			set;
		}

		/// <summary>
		/// Calculates the value of this account as it is in the present moment in time
		/// </summary>
		public decimal CalculateCurrentValue()
		{
			return CalculateValue(DateTime.Now).UpdatedValue;
		}

		/// <summary>
		/// Calculates and saves the value that this account had at some point in the past
		/// </summary>
		/// <param name="time">The time that will be used to evaluate the account</param>
		public AccountUpdate CalculateValue(DateTime time)
		{
			return CalculateValues(time, time)[0];
		}

		/// <summary>
		/// Calculates all the values that this account had in a specific period of time in the past
		/// </summary>
		/// <param name="from">Starting date used for evaluation</param>
		/// <param name="to">Ending date used for evaluation</param>
		public List<AccountUpdate> CalculateValues(DateTime from, DateTime to)
		{
			List<AccountUpdate> updates = new List<AccountUpdate>();
			List<DateTime> repetitions = TimeUnit.GetRepetitions(Start, from, to, PeriodLenght, PeriodUnit);

			foreach (DateTime r in repetitions)
			{
				DateTime r2 = TimeUnit.Add(r, PeriodLenght, PeriodUnit).AddSeconds(-1);

				//CriteriaOperator filter = CriteriaOperator.And
				//(
				//	new BinaryOperator(DateProperty, r, BinaryOperatorType.GreaterOrEqual),
				//	new BinaryOperator(DateProperty, r2, BinaryOperatorType.LessOrEqual),
				//	CriteriaOperator.Parse(EvaluationFilter)
				//);

				////if an update for this period already exist, just update it
				//AccountUpdate update;

				//update = Session.FindObject<AccountUpdate>(CriteriaOperator.And(new BinaryOperator("Date", r2), new BinaryOperator("Account", this)));

				//if (update == null)
				//{
				//	update = new AccountUpdate(Session);
				//}

				//update.Account = this;
				//update.UpdatedValue = Convert.ToDecimal(Session.Evaluate(EvaluatedType, CriteriaOperator.Parse(EvaluationExpression), filter));
				//update.Date = r2;
				//update.Save();

				//updates.Add(update);
			}

			return updates;
		}

		/// <summary>
		/// Calculates all values of this account from the Start Date to the current date
		/// </summary>
		//[Action]
		public List<AccountUpdate> CalculateAllValues()
		{
			return CalculateValues(Start, DateTime.Now);
		}
	}
}