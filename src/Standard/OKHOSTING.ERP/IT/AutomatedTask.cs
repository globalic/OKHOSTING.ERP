using OKHOSTING.RPC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.IT
{
	/// <summary>
	/// A method call with specific parameters that can be persisted and executed
	/// </summary>
	public class AutomatedTask: Production.Task
	{
		/// <summary>
		/// List of operations that will be executed, secuentially in the order that they appear
		/// </summary>
		public ICollection<Operation> Operations { get; set; }

		/// <summary>
		/// List of results of the operations that where already executed
		/// </summary>
		public ICollection<OperationResult> Results { get; set; }

		/// <summary>
		/// RPC Server where this task must be executed. If null, the task will be executed locally
		/// </summary>
		public Server Server { get; set; }

		/// <summary>
		/// Returns true if the method was already executed and failed
		/// </summary>
		public bool Failed
		{
			get;
			set;
		}

		/// <summary>
		/// Executes all aperations secuentially, if one operation fails, the hole task is stopped
		/// </summary>
		protected void Execute()
		{
			Results.Clear();
			Finished = false;
			StartDate = DateTime.Now;

			if (Server == null)
			{
				Server = new Server();
			}

			foreach (var op in Operations)
			{
				var r = Server.Execute(op);
				Results.Add(r);

				if (r.Exception != null)
				{
					Failed = true;
					break;
				}
			}

			EndDate = DateTime.Now;

			if (!Results.Where(r => r.Exception != null).Any())
			{
				Finished = true;
			}
		}

		/// <summary>
		/// Loads from the database all the automated tasks that are assigned to the current computer or do not have a computer assigned (null), 
		/// and tries to execute them all one by one
		/// </summary>
		public static void ExecuteAllPendingTasks()
		{
			var select = new ORM.Operations.Select<AutomatedTask>();
			var dtype = ORM.DataType<AutomatedTask>.GetDataType();
			var setupTasksExecuted = new List<AutomatedTask>();

			select.AddMembers(dtype.AllDataMembers);

			select.Where.Add(new ORM.Filters.ValueCompareFilter(dtype[m => m.Finished], false));
			select.Where.Add(new ORM.Filters.ValueCompareFilter(dtype[m => m.StartDate], DateTime.Now, Data.CompareOperator.LessThanEqual));
			select.OrderBy.Add(new ORM.Operations.OrderBy(dtype[m => m.StartDate]));
			select.OrderBy.Add(new ORM.Operations.OrderBy(dtype[m => m.Parent.Id]));
			select.OrderBy.Add(new ORM.Operations.OrderBy(dtype[m => m.Priority], Data.SortDirection.Descending));

			using (var db = Core.BaitAndSwitch.Create<ORM.DataBase>())
			{
				var tasks = db.Select(select);

				foreach (var task in tasks)
				{
					task.Execute();
					db.Update(task);
				}
			}
		}
	}
}