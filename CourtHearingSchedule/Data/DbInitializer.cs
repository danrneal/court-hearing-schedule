using CourtHearingSchedule.Data;
using CourtHearingSchedule.Models;
using System;
using System.Linq;

namespace CourtHearingSchedule.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CourtHearingScheduleContext context)
        {
            context.Database.EnsureCreated();

            if (context.Hearings.Any())
            {
                return;
            }

            var departments = new Department[]
            {
                new Department{Code=DepartmentCode.A, Name=DepartmentName.A},
                new Department{Code=DepartmentCode.B, Name=DepartmentName.B},
                new Department{Code=DepartmentCode.C, Name=DepartmentName.C},
                new Department{Code=DepartmentCode.D, Name=DepartmentName.D},
                new Department{Code=DepartmentCode.E, Name=DepartmentName.E}
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var cases = new Case[]
            {
                new Case{Number="C129", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C190", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C239", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C242", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C245", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C394", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C550", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C614", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C648", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C712", FilingDate=DateTime.Parse("2020-06-17")},
                new Case{Number="C824", FilingDate=DateTime.Parse("2020-06-17")}
            };

            context.Cases.AddRange(cases);
            context.SaveChanges();

            var hearings = new Hearing[]
            {
                new Hearing{Type="Accounting Hearing", DateTime=DateTime.Parse("9:00 AM"), CaseId=6, DepartmentId=1},
                new Hearing{Type="Appointment Hearing - Decedent's", DateTime=DateTime.Parse("9:00 AM"), CaseId=4, DepartmentId=1},
                new Hearing{Type="Order to Show Cause Hearing", DateTime=DateTime.Parse("10:00 AM"), CaseId=2, DepartmentId=1},
                new Hearing{Type="Status Report Hearing", DateTime=DateTime.Parse("10:00 AM"), CaseId=1, DepartmentId=1},
                new Hearing{Type="Trust Hearing", DateTime=DateTime.Parse("1:30 PM"), CaseId=7, DepartmentId=1},
                new Hearing{Type="Authority Hearing", DateTime=DateTime.Parse("2:30 PM"), CaseId=8, DepartmentId=1},
                new Hearing{Type="Authority Hearing", DateTime=DateTime.Parse("9:00 AM"), CaseId=10, DepartmentId=2},
                new Hearing{Type="Appointment Hearing - Decedent's", DateTime=DateTime.Parse("10:00 AM"), CaseId=3, DepartmentId=2},
                new Hearing{Type="Status Report Hearing", DateTime=DateTime.Parse("10:00 AM"), CaseId=9, DepartmentId=2},
                new Hearing{Type="Trust Hearing", DateTime=DateTime.Parse("10:00 AM"), CaseId=11, DepartmentId=2},
                new Hearing{Type="Order to Show Cause Hearing", DateTime=DateTime.Parse("1:30 PM"), CaseId=5, DepartmentId=2}
            };

            context.Hearings.AddRange(hearings);
            context.SaveChanges();
        }
    }
}