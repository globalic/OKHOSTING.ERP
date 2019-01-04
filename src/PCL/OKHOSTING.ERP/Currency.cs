using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// A currency that is used by the company for purchases or sales
	/// <para xml:lang="es">
	/// La moneda que es usada por le empresa para compras o ventas
	/// </para>
	/// </summary>
	/// <example>US dollar, Mexican Peso, Canadian dollar
	/// <para xml:lang="es">
	/// Dolar US, Peso Mexicano, Dolar Canadiense
	/// </para>
	/// </example>
	public class Currency
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Name of the currency
		/// <para xml:lang="es">
		/// Nombre de la moneda
		/// </para>
		/// </summary>
		/// <example>Mexican Peso
		/// <para xml:lang="es">
		/// Peso Mexicano
		/// </para>
		/// </example>
		/// <example>US Dollar
		/// <para xml:lang="es">
		/// Dolar US
		/// </para>
		/// </example>
		[RequiredValidator]
		[StringLengthValidator(50)]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// 3 letter alphabetic code that identifies this currency
		/// <para xml:lang="es">
		/// Codigo alfabético de tres letra que identifica a esta moneda
		/// </para>
		/// </summary>
		/// <example>MXN
		/// <para xml:lang="es">
		/// MXN
		/// </para>
		/// </example>
		/// <example>USD
		/// <para xml:lang="es">
		/// USD
		/// </para>
		/// </example>
		/// <remarks>
		/// You can get a complete list from http://www.currency-iso.org/iso_index/iso_tables/iso_tables_a1.htm
		/// <para xml:lang="es">
		/// Puedes obtener una lista completa en: http://www.currency-iso.org/iso_index/iso_tables/iso_tables_a1.htm
		/// </para>
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
		/// <para xml:lang="es">
		/// Rango de conversión para esta moneda
		/// </para>
		/// </summary>
		/// <remarks>
		/// Default currency must have a Rate = 1 so no conversion is made. 
		/// All other currencies must have a conversion rate related to the defauolt currency
		/// <para xml:lang="es">
		/// La moneda por defecto debe tener un rango = 1 si no se ha hecho la conversion. 
		/// Todas las otras monedas deben tener un rango relacionado a la moneda por defecto. 
		/// </para>
		/// </remarks>
		[RequiredValidator]
		public decimal Rate
		{
			get;
			set;
		}

		/// <summary>
		/// Symbol to use before the ammount
		/// <para xml:lang="es">
		/// Simbolo utilizado antes de una cantidad
		/// </para>
		/// </summary>
		/// <example>$
		/// <para xml:lang="es">
		/// $
		/// </para>
		/// </example>
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
		/// Code to use after the ammount
		/// <para xml:lang="es">
		/// Codigo utilizado despues de la cantidad
		/// </para>
		/// </summary>
		/// <example>MXN
		/// <para xml:lang="es">
		/// MXN
		/// </para>
		/// </example>
		/// <example>USD
		/// <para xml:lang="es">
		/// USD
		/// </para>
		/// </example>
		/// <example>Euro</example>
		[StringLengthValidator(20)]
		public string SymbolAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Converts this currency to another currency
		/// <para xml:lang="es">
		/// Conversion de una moneda a otra
		/// </para>
		/// </summary>
		/// <param name="currency">Currency to which this currency will be converted
		/// <para xml:lang="es">
		/// Moneda a la cual la moneda actual sera convertida
		/// </para>
		/// </param>
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

		public override string ToString()
		{
			return Name;
		}
	}
}