using OKHOSTING.ERP.New.Production;
using OKHOSTING.Data.Validation;
using OKHOSTING.Hosting.Shared;

namespace OKHOSTING.ERP.Hosting
{
	public class HostingAccountProduct : SubscriptionProduct
	{
		[RequiredValidator]
		public Plan Plan
		{
			get;
			set;
		}
	}
}