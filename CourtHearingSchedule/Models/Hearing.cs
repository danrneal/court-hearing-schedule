using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CourtHearingSchedule.Models
{
    public class HearingDateTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = DateTime.Parse(Convert.ToString(value));
            String Time = dt.ToString("t");
            List<String> TimeSlots = new List<String>() { "9:00 AM", "10:00 AM", "1:30 PM", "2:30 PM" };
            return TimeSlots.Contains(Time);
        }
    }

    public class Hearing
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public string Type { get; set; }

        [DataType(DataType.Time)]
        [HearingDateTime(ErrorMessage = "Invalid time")]
        public DateTime DateTime { get; set; }
        public int CaseId { get; set; }
        public int DepartmentId { get; set; }

        public Case Case { get; set; }
        public Department Department { get; set; }
    }
}
