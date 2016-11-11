using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP.HR
{
	/// <summary>
	/// A task that can be automated and will be performed by code
	/// </summary>
	public class AutomatedTask: Task
	{
		[RequiredValidator]
		public Type MethodType
		{
            get;
            set;
		}

		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Criteria
		{
            get;
            set;
		}

		/// <summary>
		/// Signature of the method to execute
		/// </summary>
		[StringLengthValidator(200)]
		[RequiredValidator]
		public string Method
		{
            get;
            set;
		}

		/// <summary>
		/// Paramenters that will be used to execute the method, if any
		/// </summary>
		[StringLengthValidator(StringLengthValidator.Unlimited)]
		public string Parameters
		{
            get;
            set;
		}

		[RequiredValidator]
		public bool Approved
		{
            get;
            set;
		}

		public Computer Computer
		{
            get;
            set;
		}

		//public object Execute()
		//{
		//	//inly run when this task is approved
		//	if (!Approved)
		//	{
		//		throw new Exception("Can't execute this AutomatedTask because it's not Approved yet");
		//	}

		//	//only run if this is the right computer to run it
		//	if (this.Computer != null && this.Computer.Oid != Computer.GetCurrentComputer().Oid)
		//	{
		//		throw new Exception("Can't execute this AutomatedTask on this computer because it's defined to run on another computer");
		//	}

		//	object result = null;

		//	//if a type and method have been specified, we execute the method, otherwise we just run all the subtasks
		//	if (MethodType != null && !string.IsNullOrWhiteSpace(Method))
		//	{
		//		MethodInfo method = MethodType.GetMethodFromSignature(Method);

		//		if (method == null)
		//		{
		//			throw new Exception("Can't execute this task because the specified Method does not exist");
		//		}

		//		object[] param = method.GetMethodParameters(Parameters, Session);

		//		//if method is static, just execute it
		//		if (method.IsStatic)
		//		{
		//			try
		//			{
		//				result = method.Invoke(null, param);

		//				EndOn = DateTime.Now;
		//				Progress = 100;

		//				if (result != null)
		//				{
		//					Description = result.ToString();
		//				}
		//			}
		//			catch (Exception e)
		//			{
		//				Description = e.ToString();
		//			}
		//		}
		//		else if (MethodType.IsSubclassOf(typeof(XPBaseObject)))
		//		{
		//			XPCollection objects = new XPCollection(Session, MethodType, CriteriaOperator.Parse(Criteria));

		//			//if criteria returns only 1 object, execute method
		//			if (objects.Count == 1)
		//			{
		//				try
		//				{
		//					XPBaseObject obj = (XPBaseObject)objects[0];
		//					result = method.Invoke(obj, param);
		//					obj.Save();

		//					EndOn = DateTime.Now;
		//					Progress = 100;

		//					if (result != null)
		//					{
		//						Description = result.ToString();
		//					}
		//				}
		//				catch (Exception e)
		//				{
		//					Description = e.ToString();
		//				}
		//			}

		//			//if criteria gives us 2 or more objects, create subtasks, save them and then execute them
		//			else if (objects.Count > 1)
		//			{
		//				//if there'¿s more than one object that will be affected, create one subtask for every affected object
		//				foreach (XPBaseObject obj in objects)
		//				{
		//					string subTaskCriteria = BuildCriteria(obj);

		//					//is there already a subtask for this object?
		//					AutomatedTask sub = Session.FindObject<AutomatedTask>(CriteriaOperator.And(new BinaryOperator("Parent", this), new BinaryOperator("Criteria", subTaskCriteria)));

		//					if (sub == null)
		//					{
		//						sub = new AutomatedTask(Session);
		//						sub.StartOn = DateTime.Now;
		//					}

		//					sub.Subject = string.Format("{0}: {1}", Subject, obj.ToString());
		//					sub.Parent = this;
		//					sub.MethodType = this.MethodType;
		//					sub.Method = this.Method;
		//					sub.Criteria = subTaskCriteria;
		//					sub.Parameters = this.Parameters;

		//					sub.Save();
		//				}
		//			}
		//		}
		//		//this is NOT a subclass of XPBaseObject
		//		else
		//		{
		//			try
		//			{
		//				object obj = MethodType.CreateInstance();
		//				result = method.Invoke(obj, param);

		//				EndOn = DateTime.Now;
		//				Progress = 100;

		//				if (result != null)
		//				{
		//					Description = result.ToString();
		//				}
		//			}
		//			catch (Exception e)
		//			{
		//				Description = e.ToString();
		//			}
		//		}
		//	}

		//	Save();

		//	//if subtasks where created, run them now
		//	foreach (AutomatedTask t in SubTasks)
		//	{
		//		//only execute if the task is not already finished
		//		if (!t.Finished && t.Approved)
		//		{
		//			t.Execute();
		//		}

		//		//if a subtask could not finish (raised an exception), stop the execution so we always force that all subtasks
		//		//will execute in the righr order, wich is chronologically, and no task will be "jumped", at least not automatically
		//		if (!t.Finished)
		//		{
		//			break;
		//		}
		//	}

		//	return result;
		//}

		//public void LoadFrom(XPBaseObject obj, MethodInfo method, List<object> parameters)
		//{
		//	MethodType = obj.GetType();
		//	Criteria = BuildCriteria(obj);
		//	Method = method.GetMethodSignature(true);
		//	Parameters = method.GetMethodParametersString(parameters);
		//}

		//public static void ExecuteAllPendingTasks()
		//{
		//	while (true)
		//	{
		//		XPCollection<AutomatedTask> tasks = new XPCollection<AutomatedTask>(
		//			MyApplication.XpoSession, 
		//			CriteriaOperator.And
		//			(
		//				new NullOperator("Parent"),
		//				new BinaryOperator("Finished", false), 
		//				new BinaryOperator("Approved", true), 
		//				new BinaryOperator("StartOn", DateTime.Now, BinaryOperatorType.LessOrEqual),
		//				CriteriaOperator.Or
		//				(
		//					new NullOperator("Computer"), 
		//					new BinaryOperator("Computer", Computer.GetCurrentComputer()) 
		//				)
		//			), 
		//			new SortProperty("Priority", DevExpress.Xpo.DB.SortingDirection.Ascending), 
		//			new SortProperty("StartOn", DevExpress.Xpo.DB.SortingDirection.Ascending));

		//		foreach (AutomatedTask t in tasks)
		//		{
		//			Console.WriteLine("Executing: " + t.Subject);
		//			t.Execute();
		//			Console.WriteLine("Finished: " + t.Subject);
		//		}

		//		System.Threading.Thread.Sleep(60000);
		//	}
		//}

		public void Approve()
		{
			Approved = true;
		}

		//public static AutomatedTask Find(MethodInfo method, List<object> parameters)
		//{
		//	AutomatedTask t = MyApplication.XpoSession.FindObject<AutomatedTask>(CriteriaOperator.And
		//	(
		//		new BinaryOperator("MthodType", method.DeclaringType),
		//		new BinaryOperator("Method", method.GetMethodSignature(true)),
		//		new BinaryOperator("Parameters", method.GetMethodParametersString(parameters))
		//	));

		//	return t;
		//}

		//public static AutomatedTask Find(MethodInfo method, List<object> parameters, XPBaseObject obj)
		//{
		//	AutomatedTask t = MyApplication.XpoSession.FindObject<AutomatedTask>(CriteriaOperator.And
		//	(
		//		new BinaryOperator("MethodType", method.DeclaringType),
		//		new BinaryOperator("Method", method.GetMethodSignature(true)),
		//		new BinaryOperator("Parameters", method.GetMethodParametersString(parameters)),
		//		new BinaryOperator("Criteria", BuildCriteria(obj))
		//	));

		//	return t;
		//}

		//public static string BuildCriteria(XPBaseObject obj)
		//{
		//	return string.Format("{0} = '{1}'", obj.ClassInfo.KeyProperty.Name, obj.Session.GetKeyValue(obj));
		//}

		public const string CommandDirectoryName = "OKHOSTING.ERP.HR.AutomatedTask";

		//public static string CommandDirectoryPath
		//{
		//	get
		//	{
		//		return System.IO.Path.Combine(OKHOSTING.Tools.DefaultPaths.Custom, "AutomatedTask", Computer.GetCurrentComputer().Name);
		//	}
		//}
	}
}