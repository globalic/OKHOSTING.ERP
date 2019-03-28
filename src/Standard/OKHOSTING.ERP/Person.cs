using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.Security;
using System.Collections.Generic;

namespace OKHOSTING.ERP.New
{
	/// <summary>
	/// Represents a person
	/// </summary>
	public class Person
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Prefix of the name that should be used when talking to this person
		/// </summary>
		/// <example>Sir, Miss, Misses, Dr.</example>
		[StringLengthValidator(10)]
		public string Prefix
		{
			get;
			set;
		}

		/// <summary>
		/// First name
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(50)]
		public string FirstName
		{
			get;
			set;
		}

		/// <summary>
		/// Last name
		/// </summary>
		[RequiredValidator]
		[StringLengthValidator(50)]
		public string LastName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the full name of the person in the format "FirstName LastName"
		/// </summary>
		[StringLengthValidator(100)]
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		/// <summary>
		/// Gets the "Alias" of the person, a friendly (non insultive) name to call him/her
		/// </summary>
		[StringLengthValidator(50)]
		public string Alias
		{
			get;
			set;
		}

		public Gender Gender
		{
			get;
			set;
		}

		public DateTime BirthDate
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string Telephone
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string Telephone2
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string MobileTelephone
		{
			get;
			set;
		}

		[StringLengthValidator(100)]
		[RegexValidator(Core.RegexPatterns.EmailAddress)]
		public string Email
		{
			get;
			set;
		}

		[StringLengthValidator(100)]
		[RegexValidator(Core.RegexPatterns.EmailAddress)]
		public string Email2
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Notes
		{
			get;
			set;
		}

		public int Age
		{
			get 
			{ 
				return (int) DateTime.Now.Subtract(BirthDate).TotalDays / 365; 
			}
		}

		//public User User
		//{
		//	get;
		//	set;
		//}

		public override string ToString()
		{
			return FullName;
		}
	}
}