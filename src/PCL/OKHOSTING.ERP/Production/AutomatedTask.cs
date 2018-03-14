using OKHOSTING.Data.Validation;
using System.Reflection;

namespace OKHOSTING.ERP.New.Production
{
	/// <summary>
	/// A method call with specific parameters that can be persisted and executed
	/// </summary>
	public class AutomatedTask: Task
	{
		/// <summary>
		/// If the method is not static, you must specify a persistent object instance where this method will be invoked
		/// </summary>
		public object Instance
		{
			get;
			set;
		}

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
			StartDate = System.DateTime.Now;

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
			}
			catch (System.Exception e)
			{
				Failed = true;
				Result = e;
			}
			finally
			{
				EndDate = System.DateTime.Now;
				TimeInvested = EndDate.Value - StartDate.Value;
			}
		}
	}
}