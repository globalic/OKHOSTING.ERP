using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A product or service that is active only a period of time
	/// </summary>
	/// <example>A magazine subscription, a hosting subscription, a domain name, etc.</example>
	public class SubscriptionProduct : Product
	{
		/// <summary>
		/// Lenght of the subscription
		/// </summary>
		[RequiredValidator]
		public uint SubscriptionLenght
		{
			get;
			set;
		}

		/// <summary>
		/// Unit used for subscription lenght
		/// </summary>
		[RequiredValidator]
		public Core.TimeUnit.Unit SubscriptionUnit
		{
			get;
			set;
		}

		/// <summary>
		/// Lenght of the grace period
		/// </summary>
		/// <remarks>
		/// Grace period allows to "renew" an inactive subscription. 
		/// Once the grace period is finished the subscription is deleted and a new subscription
		/// must be created
		/// </remarks>
		public uint GracePeriodLenght
		{
			get;
			set;
		}

		/// <summary>
		/// Unit used for grace period lenght
		/// </summary>
		public Core.TimeUnit.Unit GracePeriodUnit
		{
			get;
			set;
		}
	}
}