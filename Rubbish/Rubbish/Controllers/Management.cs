using System;
using Rubbish.Models;
using System.Net;

namespace Rubbish.Controllers
{
    class Management
    {

        public int GetTotalDays(Vacation vacation, Customer query)
        {
            DateTime now = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            DateTime startDate = vacation.StartDate;
            DateTime endDate = vacation.EndDate;

            int numberOfDays = GetNumberOfDays(firstDayOfMonth, lastDayOfMonth, query.DayOfWeek);

            int numberOfVacationDays = 0;

            if (vacation.StartDate == null || vacation.EndDate == null)
            {
                numberOfVacationDays = 0;
            }
            else
            {
                if (IsInMonth(vacation, startDate, endDate))
                {

                    numberOfVacationDays = GetNumberOfDays(startDate, endDate, query.DayOfWeek);
                }
            }

            int totalDays = numberOfDays - numberOfVacationDays;

            if (totalDays < 0)
            {
                totalDays = 0;
            }

            return totalDays;
        }
        private bool IsInMonth(Vacation vacation, DateTime startDate, DateTime endDate)
        {
            bool isInMonth = false;

            if (startDate.Month == DateTime.Now.Month || endDate.Month == DateTime.Now.Month)
            {
                isInMonth = true;
            }
            return isInMonth;
        }

        private int GetNumberOfDays(DateTime start, DateTime end, string day)
        {

            DayOfWeek dayOfWeek = GetDayOfWeek(day);

            TimeSpan ts = end - start;                       // Total duration
            int count = (int)Math.Floor(ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int)(ts.TotalDays % 7);         // Number of remaining days
            int sinceLastDay = (int)(end.DayOfWeek - dayOfWeek);   // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;
            {
                return count;
            }
        }


        public DayOfWeek GetDayOfWeek(string day)
        {
            var dayOfWeek = new DayOfWeek();

            switch (day)
            {
                case "Monday":
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case "Tuesday":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "Wednesday":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "Thursday":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "Friday":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "Saturday":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                default:
                    dayOfWeek = DayOfWeek.Monday;
                    break;
            }

            return dayOfWeek;
        }

    }
}
