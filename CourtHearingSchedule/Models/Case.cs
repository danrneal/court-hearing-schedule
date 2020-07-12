using System;
using System.Collections.Generic;


namespace CourtHearingSchedule.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime FilingDate { get; set; }

        public ICollection<Hearing> Hearings { get; set; }
    }
}
