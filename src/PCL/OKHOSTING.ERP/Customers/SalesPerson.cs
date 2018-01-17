using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Customers
{
	/// <summary>
	/// An employee in charge of sales, that optionally recieves a comissions for every sale he or she makes
	/// </summary>
	public class SalesPerson: HR.Employee
	{
		[RequiredValidator]
		public decimal Comission
		{
			get;
			set;
		}

		/*
		/// <summary>
		/// Calculates the total comission owned by a salesperson in a certain period
		/// </summary>
		/// <param name="start">Start date to filter</param>
		/// <param name="end">End date to filter</param>
		/// <returns>Total commission owned by the salesperson's sales</returns>
		public decimal GetTotalCommission(DateTime start, DateTime end)
		{
			decimal totalCommission = 0;

			foreach (CommissionLevel level in new XPCollection<CommissionLevel>(Session))
			{
				totalCommission += GetTotalCommission(start, end, level);
			}

			return totalCommission;
		}

		/// <summary>
		/// Calculates the total comission owned by a salesperson in a certain period and with certain commission level sales
		/// </summary>
		/// <param name="start">Start date to filter</param>
		/// <param name="end">End date to filter</param>
		/// <param name="commissionLevel">Commission level to filter</param>
		/// <returns>Total commission owned by the salesperson's sales</returns>
		public decimal GetTotalCommission(DateTime start, DateTime end, CommissionLevel commissionLevel)
		{
			decimal totalCommission = 0;
			IList<Sale> sales;

			sales = new XPCollection<Sale>(Session, CriteriaOperator.Parse("Date > {0} && Date < {1} && CommissionLevel.Oid = {3} && SalesPerson.Oid = {4}", start, end, commissionLevel, this.Oid));

			foreach (Sale sale in sales)
			{
				totalCommission += sale.Subtotal * commissionLevel.Commission;
			}

			return totalCommission;
		}

		/// <summary>
		/// Randomly assigns all customers to all active salespersons
		/// </summary>
		public static void ReAssignCustomersToSalesPersons()
		{
			XPCollection<Customer> customers = new XPCollection<Customer>(MyApplication.XpoSession, new NullOperator("SalesPerson"));
			XPCollection<SalesPerson> salesPersons = new XPCollection<SalesPerson>(MyApplication.XpoSession, new BinaryOperator("Active", true));

			SortingCollection sort = new SortingCollection();
			sort.Add(new SortProperty("Active", DevExpress.Xpo.DB.SortingDirection.Descending));
			sort.Add(new SortProperty("TotalSold", DevExpress.Xpo.DB.SortingDirection.Descending));
			
			customers.Sorting.Add(sort);
			int index = 0;

			foreach (Customer c in customers)
			{
				c.SalesPerson = (SalesPerson)salesPersons[index++];
				c.Save();

				foreach (Quote q in c.Quotes)
				{
					q.SalesPerson = c.SalesPerson;
					q.Save();
				}

				Console.WriteLine(c + " -> $" + c.TotalSold + " -> " + c.SalesPerson); 
				if (index == salesPersons.Count) index = 0;
			}
		}*/
	}
}