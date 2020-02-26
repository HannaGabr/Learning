using System;

namespace Jobby.Services.Scheduler.Models
{
    public enum Occurrence
    {
        Weekly,
        Monthly,
        Yearly
    }

    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }

    public class Schedule
    {
        public Occurrence Ocurrence { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Month Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
