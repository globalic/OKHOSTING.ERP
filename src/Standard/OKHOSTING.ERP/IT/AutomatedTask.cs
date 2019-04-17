using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Filters;
using OKHOSTING.ORM.Operations;
using System;
using System.Reflection;

namespace OKHOSTING.ERP.IT
{
	/// <summary>
	/// A method call with specific parameters that can be persisted and executed
	/// </summary>
	public class AutomatedTask: Production.Task
	{
		/// <summary>
		/// Method to execute
		/// </summary>
		[RequiredValidator]
		public MethodInfo Method
		{
			get;
			set;
		}

		/// <summary>
		/// Paramenters that will be used to execute the method, if any
		/// </summary>
		public object[] Parameters
		{
			get;
			set;
		}

		/// <summary>
		/// If the method is not static, you must specify a persistent object instance where this method will be invoked
		/// </summary>
		public object Instance
		{
			get;
			set;
		}

		/// <summary>
		/// Computer where this task must be executed. If null, the task can be executed anywhere
		/// </summary>
		public Computers.Computer Computer { get; set; }

		/// <summary>
		/// Result returned by the method, if any
		/// </summary>
		public object Result
		{
			get;
			set;
		}

		/// <summary>
		/// Returns true if the method was already executed and failed
		/// </summary>
		public bool Failed
		{
			get;
			set;
		}

		/// <summary>
		/// Executes the method and returns a generated subtask with the result
		/// </summary>
		public void Execute()
		{
			StartDate = DateTime.Now;

			try
			{
				//if method is static, just execute it
				if (Method.IsStatic)
				{
					Result = Method.Invoke(null, Parameters);
				}
				else
				{
					Result = Method.Invoke(Instance, Parameters);
				}

				Failed = false;
				Finished = true;
			}
			catch (Exception e)
			{
				Failed = true;
				Finished = false;
				Result = e;
			}
			finally
			{
				EndDate = DateTime.Now;
				TimeInvested = EndDate.Value - StartDate.Value;
			}
		}

		/// <summary>
		/// Loads from the database all the automated tasks that are assigned to the current computer or do not have a computer assigned (null), 
		/// and tries to execute them all one by one
		/// </summary>
		public static void ExecuteAllPendingTasks()
		{
			using (var db = Core.BaitAndSwitch.Create<DataBase>())
			{
				var select = new Select<AutomatedTask>();
				var dtype = DataType<AutomatedTask>.GetDataType();

				select.AddMembers
				(
					t => t.Id,
					t => t.Instance,
					t => t.Method,
					t => t.StartDate,
					t => t.EndDate,
					t => t.TimeInvested,
					t => t.Computer.Id
				);

				select.Where.Add(new ValueCompareFilter(dtype[m => m.Finished], false));
				select.Where.Add(new ValueCompareFilter(dtype[m => m.StartDate], DateTime.Now, Data.CompareOperator.LessThanEqual));

				OrFilter or = new OrFilter();
				or.InnerFilters.Add(new ValueCompareFilter(dtype[m => m.Computer.OperativeSystem.UniqueId], null, Data.CompareOperator.Equal));
				or.InnerFilters.Add(new ValueCompareFilter(dtype[m => m.Computer.OperativeSystem.UniqueId], Computers.OS.OperativeSystem.Local.UniqueId, Data.CompareOperator.Equal));
				select.Where.Add(or);

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