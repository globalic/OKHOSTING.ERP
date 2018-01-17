using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.New.Production
{
	/// <summary>
	/// Represents a Subscription to a service, wether a sale or a purchase. Allows administrators
	/// to get control of customer service subscriptions and expirations, as well as vendor's
	/// </summary>
	public class Subscription: ProductInstance
	{
		/// <summary>
		/// Whether this subscription is currently active or not
		/// </summary>
		public bool Active
		{
			get 
			{ 
				return (Start <= System.DateTime.Now) && (End >= System.DateTime.Now || End == null);
			}
		}

		/// <summary>
		/// Starting date for the subscription
		/// </summary>
		[RequiredValidator]
		public DateTime Start
		{
			get;
			set;
		}

		/// <summary>
		/// Ending date for the subscription, date when the subsciption expires
		/// </summary>
		public DateTime? End
		{
			get;
			set;
		}

		/// <summary>
		/// Ending date for the grace period, 
		/// date when the subsciption is deleted and can't be renewed any more.
		/// If a grace period ends, the subscription is deleted and a new subscription must
		/// be created
		/// </summary>
		public DateTime? GracePeriodEnd
		{
			get;
			set;
		}

		/// <summary>
		/// Whether this subscription must be automatically renewed 
		/// without manual intervention and without prior notification
		/// </summary>
		[RequiredValidator]
		public bool AutoRenew
		{
			get;
			set;
		}
	}
}