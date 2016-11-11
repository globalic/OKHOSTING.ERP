using System;
using OKHOSTING.Data.Validation;
using System.ComponentModel;
using System.Collections.Generic;

namespace OKHOSTING.ERP.HR
{
	public class ActivityCategory
	{
		/// <summary>
		/// Name of the account category
		/// </summary>
		/// <example>
		/// Sales, purchases, website traffic, marketing
		/// </example>
		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
            get;
            set;
		}

		/// <summary>
		/// More detailed description of the account category
		/// </summary>
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
            get;
            set;
		}

		/// <summary>
		/// Parent activity category
		/// </summary>
		public ActivityCategory Parent
		{
            get;
            set;
		}

		public string FullPath
		{
			get
			{
				ActivityCategory parentCategory = Parent as ActivityCategory;
				string parentPath = parentCategory != null ? parentCategory.FullPath : "";
				return parentPath + "/" + Name;
			}
		}

		public ICollection<Activity> Activities
		{
            get;
			set;
		}

		public ICollection<ActivityCategory> SubCategories
		{
            get;
			set;
		}

	}
}