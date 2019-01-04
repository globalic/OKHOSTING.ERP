using OKHOSTING.ERP.New.IT;
using OKHOSTING.ERP.New.Production;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Filters;
using OKHOSTING.ORM.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.ORM
{
	/// <summary>
	/// Searches the database for scheduled task, and generates the next round of iterations for a specified interval
		/// <para xml:lang="es">
		/// Busca la base de datos para una tarea programada y genera la siguente ronda te iteraciones para el intervalo especifico
		/// </para>
	/// </summary>
	public class TaskScheduler
	{
		/// <summary>
		/// Searches for all scheduled task that are active in the given timeframe, creates the repetitions for the each schedule and saves them in the database
		/// <para xml:lang="es">
		/// Busca todas las tareas programadas que están activas en el lapso de tiempo dado, crea las repeticiones para cada tarea y las guarda en la base de datos. 
		/// </para>
		/// </summary>
		/// <example>
		/// Use this for
		/// </example>
		public void CreateScheduledTaskRepetitions(DateTime from, DateTime to)
		{
			using (var db = DataBase.CreateDataBase())
			{
				var select = new Select<TaskSchedule>();
				var dtype = DataType<TaskSchedule>.GetMap();

				select.AddMembers
				(
					t => t.Id,
					t => t.RepeatEvery,
					t => t.RepeatUnit,
					t => t.StartDate,
					t => t.EndDate,
					t => t.Task.Id
				);

				select.Where.Add(new ValueCompareFilter(dtype[m => m.StartDate], from, Data.CompareOperator.LessThanEqual));
				select.Where.Add(new ValueCompareFilter(dtype[m => m.EndDate], to, Data.CompareOperator.GreaterThanEqual));

				var schedules = db.Select(select);

				foreach (var schedule in schedules)
				{
					schedule.Task = db.SelectInherited(schedule.Task).Last();

					var repetitions = schedule.GetRepetitions(from, to);
					
					foreach(var rep in repetitions)
					{
						db.Insert(rep);
					}
				}
			}
		}

		public void ExecutePendingAutomatedTasks()
		{
			using (var db = DataBase.CreateDataBase())
			{
				var select = new Select<AutomatedTask>();
				var dtype = DataType<AutomatedTask>.GetMap();

				select.AddMembers
				(
					t => t.Id,
					t => t.Instance,
					t => t.Method,
					t => t.StartDate,
					t => t.EndDate,
					t => t.TimeInvested
				);

				select.Where.Add(new ValueCompareFilter(dtype[m => m.Finished], false));
				select.Where.Add(new ValueCompareFilter(dtype[m => m.StartDate], DateTime.Now, Data.CompareOperator.LessThanEqual));
				select.Where.Add(new ValueCompareFilter(dtype[m => m.EndDate], DateTime.Now, Data.CompareOperator.GreaterThanEqual));

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