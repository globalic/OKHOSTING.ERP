using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.ERP.New.Production
{
	/// <summary>
	/// A schedule that will clone a base task and create a copy of it every time the schedule elapses
	/// </summary>
	public class ScheduledTask
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Unit of tiume used for repetition
		/// </summary>
		public Core.TimeUnit.Unit RepeatUnit
		{
			get;
			set;
		}

		/// <summary>
		/// Quatity used for repetition
		/// </summary>
		public int RepeatEvery
		{
			get;
			set;
		}

		/// <summary>
		/// Model Task that will be clonned in a new task every time the schedule elapses
		/// </summary>
		public Task Task { get; set; }

		/// <summary>
		/// Gets a list of copies of the base task that have a startdate for their correspondent schedule repetition.
		/// The shcedule uses the Task to get the start and end dates of the schedule
		/// </summary>
		public IEnumerable<Task> GetRepetitions()
		{
			foreach (DateTime date in Core.TimeUnit.GetRepetitions(Task.StartDate.Value, Task.EndDate.Value, Task.EndDate.Value, RepeatEvery, RepeatUnit))
			{
				//create a new clone of the base task
				Task newTask = Task.Clone();

				//but with a different date adn set as a subtask of the original
				newTask.StartDate = date;
				newTask.Name = $"{Task.Name} / {DateTime.Now.ToString("yyyy-MM-dd hh:mm")}";

				//set these to empty
				newTask.Id = Guid.Empty;
				newTask.Progress = 0;
				newTask.EndDate = null;
				newTask.TimeInvestedTotal = newTask.TimeInvested = TimeSpan.Zero;

				yield return newTask;
			}
		}
	}
}