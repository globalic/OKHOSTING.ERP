using System;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A product that is included into a PackageProduct
	/// </summary>
	public class PackageProductIncludedProduct
	{
		public Guid Id { get; set; }

		public PackageProduct PackageProduct
		{
			get;
			set;
		}

		public Product IncludedProduct
		{
			get;
			set;
		}

		public Int32 Quantity
		{
			get;
			set;
		}

	}
}