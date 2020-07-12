using System.Collections.Generic;


namespace CourtHearingSchedule.Models
{
    public enum DepartmentCode
    {
        A, B, C, D, E
    }

    public enum DepartmentName
    {
        A, B, C, D, E
    }

    public class Department
    {
        public int Id { get; set; }
        public DepartmentCode Code { get; set; }
        public DepartmentName Name { get; set; }

        public ICollection<Hearing> Hearings { get; set; }
    }
}
