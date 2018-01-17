using OKHOSTING.ERP.New.Production;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.ORM
{
	public static class TaskExtensions
	{
		public static void OnBeforeInsert(this Task task, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeInsert(sender, eventArgs);

			if (task.Parent != null)
			{
				if (task.Customer == null)
				{
					task.Customer = task.Parent.Customer;
				}

				if (task.AssignedTo == null)
				{
					task.AssignedTo = task.Parent.AssignedTo;
				}
			}
		}

		public static void OnBeforeUpdate(this Task task, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnBeforeUpdate(sender, eventArgs);

			if (task.Parent != null)
			{
				if (task.Customer == null)
				{
					task.Customer = task.Parent.Customer;
				}

				if (task.AssignedTo == null)
				{
					task.AssignedTo = task.Parent.AssignedTo;
				}
			}
		}

		public static void OnAfterInsert(this Task task, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterInsert(sender, eventArgs);

			if (task.Parent != null)
			{
				task.Parent.Select();
				task.Parent.RecalculateValues();
				task.Parent.Save();
			}
		}

		public static void OnAfterUpdate(this Task task, DataBase sender, OperationEventArgs eventArgs)
		{
			//base.OnAfterUpdate(sender, eventArgs);

			if (task.Parent != null)
			{
				task.Parent.Select();
				task.Parent.RecalculateValues();
				task.Parent.Save();
			}
		}
	}
}