using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CourtHearingSchedule.Data;
using CourtHearingSchedule.Models;

namespace CourtHearingSchedule.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CourtHearingSchedule.Data.CourtHearingScheduleContext _context;

        public IndexModel(CourtHearingSchedule.Data.CourtHearingScheduleContext context)
        {
            _context = context;
        }

        public IList<Hearing> Hearings { get; set; }
        public IList<Department> Departments { get; set; }

        public async Task OnGetAsync()
        {
            Hearings = await _context.Hearings
                .Include(h => h.Case)
                .Include(h => h.Department)
                .OrderBy(h => h.Number)
                .ThenBy(h => h.DateTime)
                .ThenBy(h => h.Type)
                .ThenBy(h => h.Case.Number).ToListAsync();

            Departments = await _context.Departments
                .OrderBy(d => d.Name).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Request.Form["type"] == "start")
            {
                Hearings = await _context.Hearings
                    .Include(h => h.Case)
                    .Include(h => h.Department)
                    .OrderBy(h => h.Number)
                    .ThenBy(h => h.DateTime)
                    .ThenBy(h => h.Type)
                    .ThenBy(h => h.Case.Number).ToListAsync();

                Departments = await _context.Departments
                    .OrderBy(d => d.Name).ToListAsync();

                foreach (Department department in Departments)
                {
                    DateTime CurrentDateTime = DateTime.MinValue;
                    int HearingNumber = 0;
                    foreach (Hearing hearing in Hearings)
                    {
                        if (hearing.Department != department)
                        {
                            continue;
                        }

                        if (CurrentDateTime != hearing.DateTime)
                        {
                            CurrentDateTime = hearing.DateTime;
                            HearingNumber = (TimeSlots.Times.IndexOf(hearing.DateTime.ToString("t")) + 1) * 1000;
                        }

                        hearing.Number = HearingNumber;
                        HearingNumber++;
                    }
                }
            }
            else if (Request.Form["type"] == "reset")
            {
                foreach (Hearing hearing in _context.Hearings)
                {
                    hearing.Number = null;
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
