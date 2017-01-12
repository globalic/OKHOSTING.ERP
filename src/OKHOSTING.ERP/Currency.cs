using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A currency that is used by the company for purchases or sales
	/// </summary>
	/// <example>US dollar, Mexican Peso, Canadian dollar</example>
	public class Currency : ORM.PersistentClass<Guid>
	{
		/// <summary>
		/// Name of the currency
		/// </summary>
		/// <example>Mexican Peso</example>
		/// <example>US Dollar</example>
		[RequiredValidator]
		[StringLengthValidator(50)]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// 3 letter alphabetic code that identifies this currency
		/// </summary>
		/// <example>MXN</example>
		/// <example>USD</example>
		/// <remarks>
		/// You can get a complete list from http://www.currency-iso.org/iso_index/iso_tables/iso_tables_a1.htm
		/// </remarks>
		[RequiredValidator]
		[StringLengthValidator(3)]
		public string AlphabeticCode
		{
			get;
			set;
		}

		/// <summary>
		/// Conversion rate for this currency
		/// </summary>
		/// <remarks>
		/// Default currency must have a Rate = 1 so no conversion is made. 
		/// All other currencies must have a conversion rate related to the defauolt currency
		/// </remarks>
		[RequiredValidator]
		public decimal Rate
		{
			get;
			set;
		}

		/// <summary>
		/// Symbol to use before the ammount
		/// </summary>
		/// <example>$</example>
		/// <example>€</example>
		/// <example>¥</example>
		[RequiredValidator]
		[StringLengthValidator(20)]
		public string SymbolBefore
		{
			get;
			set;
		}

		/// <summary>
		/// Symbol to use before the ammount
		/// </summary>
		/// <example>MXN</example>
		/// <example>USD</example>
		/// <example>Euro</example>
		[StringLengthValidator(20)]
		public string SymbolAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Converts this currency to another currency
		/// </summary>
		/// <param name="currency">Currency to which this currency will be converted</param>
		public decimal ConvertTo(Currency currency)
		{
			throw new NotImplementedException();
			//return this.Rate * currency.Rate;
		}

		///// <summary>
		///// Downloads real-time conversion rate from a webservice
		///// </summary>
		///// <remarks>
		///// Uses free webservice located at http://www.webservicex.net/CurrencyConvertor.asmx?WSDL
		///// </remarks>
		//[Action]
		//public void DownloadRate()
		//{
		//	OKHOSTING.Services.CurrencyConvertor.Currency from, to;

		//	from = (OKHOSTING.Services.CurrencyConvertor.Currency)Enum.Parse(typeof(OKHOSTING.Services.CurrencyConvertor.Currency), this.AlphabeticCode);
		//	to = (OKHOSTING.Services.CurrencyConvertor.Currency)Enum.Parse(typeof(OKHOSTING.Services.CurrencyConvertor.Currency), Default.AlphabeticCode);

		//	OKHOSTING.Services.CurrencyConvertor.CurrencyConvertorSoapClient client = new OKHOSTING.Services.CurrencyConvertor.CurrencyConvertorSoapClient();
		//	this.Rate = (decimal)client.ConversionRate(from, to);
		//}

		///// <summary>
		///// Default currency
		///// </summary>
		//public static readonly Currency Default;

		///// <summary>
		///// Initiates static properties
		///// </summary>
		//static Currency()
		//{
		//	using (IObjectSpace db = WebApplication.Instance.CreateObjectSpace())
		//	{
		//		Default = db.FindObject<Currency>(new BinaryOperator("Name"
		//	}
		//}
	}
}