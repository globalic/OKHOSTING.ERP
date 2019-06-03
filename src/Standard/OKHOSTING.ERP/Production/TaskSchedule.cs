using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Filters;
using OKHOSTING.ORM.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.Production
{
	/// <summary>
	/// A schedule that will clone a base task and create a copy of it every time the schedule elapses
	/// </summary>
	public class TaskSchedule: Core.Schedule
	{
		/// <summary>
		/// Model Task that will be clonned in a new task every time the schedule elapses
		/// </summary>
		[RequiredValidator]
		public Task Task { get; set; }

		/// <summary>
		/// Gets a list of copies of the base task that have a startdate for their correspondent schedule repetition.
		/// The shcedule uses the Task to get the start and end dates of the schedule
		/// </summary>
		public IEnumerable<Task> GetRepetitions(DateTime from, DateTime to)
		{
			foreach (DateTime date in Core.TimeUnit.GetRepetitions(Task.StartDate.Value, from, to, RepeatEvery, RepeatUnit))
			{
				//create a new clone of the base task
				Task newTask = Task.Clone();

				//but with a different date adn set as a subtask of the original
				newTask.StartDate = date;
				newTask.Name = $"{Task.Name} / {DateTime.Now.ToString("yyyy-MM-dd hh:mm")}";
				newTask.Parent = Task;

				//set these to empty
				newTask.Id = Guid.Empty;
				newTask.Progress = 0;

				if (Task.StartDate.HasValue && Task.EndDate.HasValue)
				{
					newTask.EndDate = date + (Task.EndDate.Value - Task.StartDate.Value);
				}
				else
				{
					newTask.EndDate = null;
				}

				newTask.TimeInvestedTotal = newTask.TimeInvested = TimeSpan.Zero;

				yield return newTask;
			}
		}

		/// <summary>
		/// Searches for al scheduled task that are active in the given timeframe, creates the repetitions for the each schecule and saves them in the database
		/// </summary>
		/// <example>
		/// Use this for
		/// </example>
		public static void CreateScheduledTaskRepetitions(DateTime from, DateTime to)
		{
			using (var db = Core.BaitAndSwitch.Create<DataBase>())
			{
				var select = new Select<TaskSchedule>();
				var dtype = DataType<TaskSchedule>.GetDataType();

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
					var operations = new List<Insert>();

					foreach (var rep in repetitions)
					{
						operations.Add(new Insert() { Instance = rep });
					}

					db.Execute(operations);
				}
			}
		}
	}
}