using System;
using OKHOSTING.Data.Validation;
using OKHOSTING.ERP.HR;
using OKHOSTING.ERP;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.HR
{
	public class Activity
	{
		[StringLengthValidator(100)]
		[RequiredValidator]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Duration of the activity
		/// </summary>
		public TimeSpan? TotalDuration
		{
			get
			{
				if (StartDate != null && EndDate != null)
				{
					return EndDate - StartDate;
				}
				else
				{
					return TimeSpan.Zero;
				}
			}
		}

		public bool Finished
		{
			get
			{
				return Progress == 100;
			}
		}

		[RequiredValidator]
		public Employee AssignedTo
		{
			get;
			set;
		}

		[RequiredValidator]
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Percentaje (from 0 to 100) of progress, how much is an activity finished
		/// </summary>
		[RequiredValidator]
		[RangeValidator(0, 100)]
		public ushort Progress
		{
			get;
			set;
		}

		//[Custom("DisplayFormat", "{0:G}")]
		//[Custom("EditMask", "G")]
		//public DateTime? StartDate
		//{
		//	get
		//	{
		//		return Convert.ToDateTime(Updates.Min(StartDate));
		//	}
		//}

		//[Custom("DisplayFormat", "{0:G}")]
		//[Custom("EditMask", "G")]
		//public DateTime? EndDate
		//{
		//	get
		//	{
		//		return Convert.ToDateTime(Updates.Max(EndDate));
		//	}
		//}

		/// <summary>
		/// Time invested (in hours) in performing the activity or part of the activity
		/// </summary>
		//[Custom("DisplayFormat", "{0:G}")]
		//public decimal HoursInvested
		//{
		//	get 
		//	{
		//		return Convert.ToDecimal(Updates.Sum(HoursInvested));
		//	}
		//}

		public ActivityCategory Category
		{
			get;
			set;
		}

		/// <summary>
		/// The company (customer or vendor) which this activity is related to, if any.
		/// </summary>
		public Company Company
		{
			get;
			set;
		}

		/// <summary>
		/// InvoiceItem that is related to this activity in the way it involved a sale or a purchase.
		/// </summary>
		/// <remarks>
		/// Used to link activities with sales and purchases, and keep tracking of how much money is spent
		/// or earned realted to the ammount of time invested in activities to complete the sale or purchase
		/// </remarks>
		public InvoiceItem InvoiceItem
		{
			get;
			set;
		}

		public ActivityPriority Priority
		{
			get;
			set;
		}

		public Project Project
		{
			get;
			set;
		}

		public ICollection<ActivityUpdate> Updates
		{
			get;
			set;
		}

		public void MarkAsFinished(decimal hoursInvested)
		{
			//ActivityUpdate update = new ActivityUpdate(Session);
			//update.Activity = this;
			//update.StartDate = DateTime.Now.AddHours((double) hoursInvested * -1);
			//update.HoursInvested = hoursInvested;
			//update.Employee = (Employee) OKHOSTING.Security.User.Current.BelongsTo;
		}
	}
}