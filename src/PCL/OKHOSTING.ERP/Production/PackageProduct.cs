using System;
using System.Collections.Generic;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A product that is a "package" of products, all included products are sold or purchased by a fixed price
	/// </summary>
	public class PackageProduct : Product
	{
		public ICollection<PackageProductIncludedProduct> IncludedProducts
		{
			get;
			set;
		}
	}
}