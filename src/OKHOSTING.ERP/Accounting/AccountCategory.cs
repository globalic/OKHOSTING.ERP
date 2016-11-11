using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using System.ComponentModel;

namespace OKHOSTING.ERP.Accounting
{
	[NavigationItem]
	public class AccountCategory : OKBaseObject, ITreeNode
	{
		/// <summary>
		/// Name of the account category
		/// </summary>
		/// <example>
		/// Sales, purchases, website traffic, marketing
		/// </example>
		[RuleRequiredField]
		[Size(100)]
		public string Name
		{
			get { return GetPropertyValue<String>("Name"); }
			set { SetPropertyValue("Name", value); }
		}

		/// <summary>
		/// More detailed description of the account category
		/// </summary>
		[Size(SizeAttribute.Unlimited)]
		public string Description
		{
			get { return GetPropertyValue<String>("Description"); }
			set { SetPropertyValue("Description", value); }
		}

		/// <summary>
		/// Parent account category
		/// </summary>
		[Association]
		public AccountCategory Parent
		{
			get { return GetPropertyValue<AccountCategory>("Parent"); }
			set { SetPropertyValue("Parent", value); }
		}

		public string FullPath
		{
			get
			{
				AccountCategory parent = Parent as AccountCategory;
				string parentPath = parent != null ? parent.FullPath : "";
				return parentPath + "/" + Name;
			}
		}

		[Association]
		public XPCollection<AccountCategory> SubCategories
		{
			get { return GetCollection<AccountCategory>("SubCategories"); }
		}

		[Association]
		public XPCollection<Account> Accounts
		{
			get { return GetCollection<Account>("Accounts"); }
		}

		#region ITreeNode

		IBindingList ITreeNode.Children
		{
			get
			{
				return SubCategories;
			}
		}

		string ITreeNode.Name
		{
			get
			{
				return Name;
			}
		}

		ITreeNode ITreeNode.Parent
		{
			get
			{
				return Parent;
			}
		}

		#endregion

		public AccountCategory(): base(MyApplication.XpoSession)
		{
		}
		
		public AccountCategory(DevExpress.Xpo.Session session): base(session)
		{
		}
	}
}