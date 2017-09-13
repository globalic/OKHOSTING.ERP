using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A category for products
	/// </summary>
	public class ProductCategory : ORM.Model.Base<Guid>
	{
		public ProductCategory Parent
		{
			get;
			set;
		}

		[RequiredValidator]
		[StringLengthValidator(100)]
		public string Name
		{
			get;
			set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		public ICollection<ProductCategory> SubCategories
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}