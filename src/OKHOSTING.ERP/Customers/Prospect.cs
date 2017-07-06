using System;
using OKHOSTING.Data;
using OKHOSTING.ERP.HR;
using OKHOSTING.ERP.Production;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Customers
{
	/// <summary>
	/// A company or individual that can potentialy become a customer
	/// </summary>
	public class Prospect : Company
	{
		/// <summary>
		/// Sales person who is responsible for this prospect
		/// </summary>
		public SalesPerson SalesPerson
		{
			get;
			set;
		}

		/// <summary>
		/// Main product this prospect is interested in purchasing
		/// </summary>
		public Product ProductOfInterest
		{
			get;
			set;
		}

		/// <summary>
		/// Total ammount of the potential sale
		/// </summary>
		[RequiredValidator]
		public decimal PotentialSaleAmmount
		{
			get;
			set;
		}

		/// <summary>
		/// Status of the prospect
		/// </summary>
		[RequiredValidator]
		public ProspectStatus Status
		{
			get;
			set;
		}

		/// <summary>
		/// Source of the prospect
		/// </summary>
		[RequiredValidator]
		public ProspectSource Source
		{
			get;
			set;
		}

		/// <summary>
		/// Date when the SalesPerson should be calling/emailing the prospect next time
		/// </summary>
		[RequiredValidator]
		public DateTime NextCallDate
		{
			get;
			set;
		}

		/// <summary>
		/// Date qhen the SalesPerson should be calling/emailing the prospect next time
		/// </summary>
		[RequiredValidator]
		public DateTime RegistrationDate
		{
			get;
			set;
		}


		/// <summary>
		/// Converts the current Prospect into a Customer
		/// </summary>
		/// <returns>Resulting Customer</returns>
		//public Customer ConvertToCustomer()
		//{
		//	Customer customer = new Customer();

		//	//copy values
		//	this.CopyTo(customer);

		//	//copy collections
		//	customer.Addresses = DataBase.Current.SelectByForeignKey((DataValue)DataType.From(typeof(CompanyAddress))["Company"], this);
		//	customer.Contacts = DataBase.Current.SelectByForeignKey((DataValue)DataType.From(typeof(Employee))["Company"], this);

		//	//delete me
		//	DataBase.Current.Delete(this);

		//	//insert customer with the same id
		//	DataBase.Current.Insert(customer);
		//	DataBase.Current.Insert(customer.Addresses);
		//	DataBase.Current.Insert(customer.Contacts);

		//	return customer;
		//}
	}
}