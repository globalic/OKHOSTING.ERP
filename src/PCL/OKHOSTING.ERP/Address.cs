using OKHOSTING.Data.Validation;
using System;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// An address of a person or a company
	/// <para xml:lang="es">
	/// La direccion de una persona o una empresa
	/// </para>
	/// </summary>
	public class Address
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Name of the street
		/// <para xml:lang="es">
		/// Nombre de la calle
		/// </para>
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
		/// <para xml:lang="es">
		/// Numero de la direccion
		/// </para>
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
		/// <para xml:lang="es">
		/// Colonia
		/// </para>
		/// </summary>
		[StringLengthValidator(30)]
		public string Suburb
		{
			get;
			set;
		}

		/// <summary>
		/// Zip code
		/// <para xml:lang="es">
		/// Codigo Postal
		/// </para>
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
		/// <para xml:lang="es">
		/// Ciudad
		/// </para>
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
		/// <para xml:lang="es">
		/// Estado
		/// </para>
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
		/// <para xml:lang="es">
		/// Pais
		/// </para>
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
		/// <para xml:lang="es">
		/// Notes adicionales sobre la direccion
		/// </para>
		/// </summary>
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Notes
		{
			get;
			set;
		}

		/// <summary>
		/// Geographic coordinates in decimal notation
		/// <para xml:lang="es">
		/// Coordenadas geográficas en notacion decimal
		/// </para>
		/// </summary>
		public Tuple<decimal, decimal> Coordinates
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