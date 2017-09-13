using System;
using System.Collections.Generic;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A unit for meassuring time
	/// </summary>
	public static class TimeUnit
	{
		/// <summary>
		/// Adds a tTimeUnit to a DateTime
		/// </summary>
		/// <param name="startDate">DateTime that will be added a TimeUnit</param>
		/// <param name="lenght">Lenght of the TimeUnit to add</param>
		/// <param name="unit">TimeUnit to add</param>
		/// <returns>The resulting DateTime off adding the TimeUnit to startDate</returns>
		public static DateTime Add(DateTime startDate, int lenght, Unit unit)
		{
			DateTime d = startDate;

			switch (unit)
			{
				case Unit.Second:
					d = startDate.AddSeconds(lenght);
					break;

				case Unit.Minute:
					d = startDate.AddMinutes(lenght);
					break;

				case Unit.Hour:
					d = startDate.AddHours(lenght);
					break;

				case Unit.Day:
					d = startDate.AddDays(lenght);
					break;

				case Unit.Week:
					d = startDate.AddDays(lenght * 7);
					break;

				case Unit.Month:
					d = startDate.AddMonths((int)lenght);
					break;

				case Unit.Year:
					d = startDate.AddYears((int)lenght);
					break;
			}

			return d;
		}

		/// <summary>
		/// Gets all date repetitions that happens in an interval of time
		/// </summary>
		/// <param name="periodStartDate">
		/// Date when the period starts, for example, the date when a paid subscription started
		/// </param>
		/// <param name="startDate">
		/// Only repetitions that have a date equal or bigger than this argument will be included in the result
		/// </param>
		/// <param name="endDate">
		/// Only repetitions that have a date less or lower than this argument will be included in the result
		/// </param>
		/// <param name="periodLenght">
		/// Lenght of the repetition unit
		/// </param>
		/// <param name="periodUnit">
		/// Time unit of the repetition
		/// </param>
		/// <returns>
		/// A list containing the dates of the recurring repetitions inside the specified date interval
		/// </returns>
		public static List<DateTime> GetRepetitions(DateTime periodStartDate, DateTime startDate, DateTime endDate, int periodLenght, Unit periodUnit)
		{
			if (endDate < startDate)
			{
				throw new ArgumentOutOfRangeException("startDate", "Argument must be older than the endDate argument");
			}
			
			if (startDate < periodStartDate)
			{
				throw new ArgumentOutOfRangeException("periodStartDate", "Argument must be older than the startDate argument");
			}

			List<DateTime> periods = new List<DateTime>();
			DateTime p = periodStartDate;

			do
			{
				if (p >= startDate)
				{
					periods.Add(p);
				}

				p = TimeUnit.Add(p, periodLenght, periodUnit);
			}
			while (p <= endDate);

			return periods;
		}

		/// <summary>
		/// Returns the exact date & time when the last repetition occurred before the specified date
		/// </summary>
		/// <param name="periodStartDate">
		/// Date when the period starts, for example, the date when a paid subscription started
		/// </param>
		/// <param name="referenceDate">
		/// The repetition that happens right before this date will be returned
		/// </param>
		/// <param name="periodLenght">
		/// Lenght of the repetition unit
		/// </param>
		/// <param name="periodUnit">
		/// Time unit of the repetition
		/// </param>
		public static DateTime GetLastRepetition(DateTime periodStartDate, DateTime referenceDate, int periodLenght, Unit periodUnit)
		{
			//add an adittional period so we include this date in the search
			DateTime previous = TimeUnit.Add(referenceDate, periodLenght * -1, periodUnit);

			//there should always be 1 and only 1 result here
			return GetRepetitions(periodStartDate, previous, referenceDate, periodLenght, periodUnit)[0];
		}

		/// <summary>
		/// Returns the exact date & time when the next repetition will occur starting from the specified date
		/// </summary>
		public static DateTime GetNextRepetition(DateTime periodStartDate, DateTime referenceDate, int periodLenght, Unit periodUnit)
		{
			//add an adittional period so we include this date in the search
			DateTime next = TimeUnit.Add(referenceDate, periodLenght, periodUnit);

			//there should always be 1 and only 1 result here
			return GetRepetitions(periodStartDate, referenceDate, next, periodLenght, periodUnit)[0];
		}

		/// <summary>
		/// A unit for meassuring time
		/// </summary>
		public enum Unit
		{
			Second = 0,
			Minute = 1,
			Hour = 2,
			Day = 3,
			Week = 4,
			Month = 5,
			Year = 6,
		}
	}
}