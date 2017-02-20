using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using System;
using System.Collections.Generic;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A company
	/// </summary>
	/// <remarks>This is the base class for customers, vendors, competition and any kind of company</remarks>
	public class Company : ORM.PersistentClass<Guid>
	{
		[RequiredValidator]
		[StringLengthValidator(100)]
		public string LegalName
		{
			get;
			set;
		}

		[StringLengthValidator(100)]
		public string CommercialName
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string FederalTaxID
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

		[StringLengthValidator(100)]
		[RegexValidator(Core.RegexPatterns.DomainName)]
		public string Url
		{
			get;
			set;
		}

		public CompanyAddress MainAddress
		{
			get;
			set;
		}

		public CompanyContact MainContact
		{
			get;
			set;
		}

		#region Collections

		public ICollection<CompanyAddress> Addresses
		{
			get;
			set;
		}

		public ICollection<CompanyContact> Contacts
		{
			get;
			set;
		}

		//public ICollection<HR.Task> Tasks
		//{
  //		  get;
		//	set;
		//}

		public string AllEmails
		{
			get
			{
				string emails = string.Empty;

				foreach (CompanyContact contact in Contacts)
				{
					if (contact.Email != null && !emails.Contains(contact.Email))
					{
						emails += contact.Email + ',' + ' ';
					}
					if (contact.Email2 != null && !emails.Contains(contact.Email2))
					{
						emails += contact.Email2 + ',' + ' ';
					}
				}

				if (Email != null && !emails.Contains(Email))
				{
					emails += Email + ',' + ' ';
				}
				if (Email2 != null && !emails.Contains(Email2))
				{
					emails += Email2 + ',' + ' ';
				}

				emails = emails.Trim(',', ' ');

				return emails;
			}
		}

		public string AllTelephones
		{
			get
			{
				string telephones = string.Empty;

				foreach (CompanyContact contact in Contacts)
				{
					if (contact.Telephone != null && !telephones.Contains(contact.Telephone))
					{
						telephones += contact.Telephone + ',' + ' ';
					}
					if (contact.Telephone2 != null && !telephones.Contains(contact.Telephone2))
					{
						telephones += contact.Telephone2 + ',' + ' ';
					}
					if (contact.MobileTelephone != null && !telephones.Contains(contact.MobileTelephone))
					{
						telephones += contact.MobileTelephone + ',' + ' ';
					}
				}

				if (Telephone != null && !telephones.Contains(Telephone))
				{
					telephones += Telephone + ',' + ' ';
				}
				if (Telephone2 != null && !telephones.Contains(Telephone2))
				{
					telephones += Telephone2 + ',' + ' ';
				}
				if (MobileTelephone != null && !telephones.Contains(MobileTelephone))
				{
					telephones += MobileTelephone + ',' + ' ';
				}

				telephones = telephones.Trim(',', ' ');

				return telephones;
			}
		}

		#endregion

		public CompanyAddress SearchAddress(CompanyAddressType type)
		{
			if (Addresses == null) return null;
			if (Addresses.Count == 0) return null;

			foreach (CompanyAddress a in Addresses)
			{
				if (a.AddressType == type) return a;
			}

			return null;
		}

		/// <summary>
		/// Deletes all addresses and  contacts of this company
		/// </summary>
		protected override void OnBeforeDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			base.OnBeforeDelete(sender, eventArgs);

			sender.LoadCollection(this, i => i.Addresses);

			foreach (var a in Addresses)
			{
				sender.Delete(a);
			}

			sender.LoadCollection(this, i => i.Contacts);

			foreach (var c in Contacts)
			{
				sender.Delete(c);
			}
		}

		public override string ToString()
		{
			return LegalName;
		}
	}
}