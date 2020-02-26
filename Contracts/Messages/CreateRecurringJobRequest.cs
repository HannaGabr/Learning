using Jobby.Services.Scheduler.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Jobby.Contracts.Messages
{
    public class CreateRecurringJobRequest
    {
        public string Description { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Occurrence Occurrence { get; set; }
        public int DayOfMonth { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DayOfWeek DayOfWeek { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Month Month { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
