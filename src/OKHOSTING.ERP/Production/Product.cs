using System;
using OKHOSTING.ERP.Finances;
using OKHOSTING.ERP.Vendors;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A product that can be purchased or saled
	/// </summary>
	public class Product : ORM.Model.Base<Guid>
	{
		[RequiredValidator]
		public ProductCategory Category
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

		[StringLengthValidator(20)]
		public string ShortName
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string AuxId
		{
			get;
			set;
		}

		[StringLengthValidator(300)]
		public Type ProductInstanceType
		{
			get;
			set;
		}

		#region Sales

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		public decimal Price
		{
			get;
			set;
		}

		[RequiredValidator]
		public bool Sale
		{
			get;
			set;
		}

		[RequiredValidator]
		public TaxGroup SaleTaxes
		{
			get;
			set;
		}

		#endregion

		#region Purchasing

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string PurchaseDescription
		{
			get;
			set;
		}

		public decimal PurchasePrice
		{
			get;
			set;
		}

		public Vendor PreferredVendor
		{
			get;
			set;
		}

		[StringLengthValidator(50)]
		public string PreferredVendorItemID
		{
			get;
			set;
		}

		[RequiredValidator]
		public bool Purchase
		{
			get;
			set;
		}

		[RequiredValidator]
		public TaxGroup PurchaseTaxes
		{
			get;
			set;
		}

		public decimal StandardCost
		{
			get;
			set;
		}

		#endregion

		/*
		[AdvancedAction]
		public void Merge(Product product)
		{
			if (product.Oid == this.Oid)
			{
				throw new ArgumentException("Can't merge the same product", "product");
			}

			foreach(InvoiceItem item in new ICollection<InvoiceItem>(new BinaryOperator("Product", product)))
			{
				item.Product = this;
				item.Save();
			}

			//delete the other customer
			product.Delete();

			Save();
		}
		*/

		public override string ToString()
		{
			return Name;
		}
	}
}