using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Production
{
	public interface ISubscription
	{
		/// <summary>
		/// Whether this subscription is currently active or not
		/// </summary>
		Boolean Active
		{
			get;
		}

		/// <summary>
		/// Starting date for the subscription
		/// </summary>
		DateTime Start
		{
			get;
			set;
		}

		/// <summary>
		/// Ending date for the subscription, date when the subsciption expires
		/// </summary>
		DateTime? End
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
		DateTime? GracePeriodEnd
		{
			get;
			set;
		}

		/// <summary>
		/// Whether this subscription must be automatically renewed 
		/// without manual intervention and without prior notification
		/// </summary>
		Boolean AutoRenew
		{
			get;
			set;
		}
	}
}