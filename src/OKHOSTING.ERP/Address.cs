using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// An address of a person or a company
	/// </summary>
	public class Address
	{
		public System.Guid Id
		{
			get;
			set;
		}

		/// <summary>
		/// Name of the street
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(200)]
		public string Street
		{
			get;
			set;
		}

		/// <summary>
		/// Number of the address
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(30)]
		public string Number
		{
			get;
			set;
		}

		/// <summary>
		/// Suburb
		/// </summary>
		[StringLengthValidator(30)]
		public string Suburb
		{
			get;
			set;
		}

		/// <summary>
		/// Zip code
		/// </summary>
		/// <example>44510, 90210</example>
		[RequiredValidator]
		[StringLengthValidator(10)]
		public string ZipCode
		{
			get;
			set;
		}

		/// <summary>
		/// City
		/// </summary>
		/// <example>Guadalajara, San Francisco</example>
		[RequiredValidator]
		[StringLengthValidator(30)]
		public string City
		{
			get;
			set;
		}

		/// <summary>
		/// State
		/// </summary>
		/// <example>Jalisco, Aguascalientes</example>
		[RequiredValidator]
		[StringLengthValidator(30)]
		public string State
		{
			get;
			set;
		}

		/// <summary>
		/// Country
		/// </summary>
		/// <example>México</example>
		[RequiredValidator]
		public Country Country
		{
			get;
			set;
		}

		/// <summary>
		/// Adittional notes on the address
		/// </summary>
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Notes
		{
			get;
			set;
		}

		public string FulllAddress
		{
			get
			{
				return string.Format(
					@"{0} #{1}
					{2} {3}, {4}, {5}
					{6}", Street, Number, ZipCode, City, State, Country, Notes);
			}
		}

	}
}