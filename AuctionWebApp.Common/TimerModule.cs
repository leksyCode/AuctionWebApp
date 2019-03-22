using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionWebApp
{
    public class TimerModule
    { 
        public static string DifferenceTime(DateTime firstDate)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan span = firstDate - currentDate;
            string leftMinutes = null, leftHourses = null, leftDays = null, leftSec = null;

            if (span.Minutes != 0)
            {
                leftMinutes = Math.Abs(span.Minutes).ToString() + "m ";
            }

            if (span.Hours != 0)
            {
                leftHourses = Math.Abs(span.Hours).ToString() + "h ";
            }

            if (span.Days != 0)
            {
                leftDays = Math.Abs(span.Days).ToString() + "d ";
            }

            if (span.Days == 0 && span.Hours == 0 && span.Minutes == 0)
            {
                leftSec = span.Seconds.ToString() + " seconds";
            }

            return leftDays + " " + leftHourses + " " + leftMinutes + leftSec;
        }
    }
}
