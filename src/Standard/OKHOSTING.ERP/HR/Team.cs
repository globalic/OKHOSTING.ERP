using System;
using System.Collections.Generic;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A group of employees that work togheter, in close coordination for a common goal
	/// </summary>
	public class Team
	{
		public Guid Id { get; set; }

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

		/// <summary>
		/// The leader of the team
		/// </summary>
		public Employee Leader
		{
			get;
			set;
		}

		/// <summary>
		/// Product category that this team is realted to
		/// </summary>
		public Production.ProductCategory ProductCategory
		{
			get;
			set;
		}
		
		public Team Parent
		{
			get;
			set;
		}

		/// <summary>
		/// Members of the team, that are under the direct command of the leader
		/// </summary>
		public ICollection<Employee> Members
		{
			get;
			set;
		}

		public ICollection<Team> SubTeams
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