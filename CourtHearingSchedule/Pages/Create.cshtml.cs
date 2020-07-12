using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourtHearingSchedule.Data;
using CourtHearingSchedule.Models;

namespace CourtHearingSchedule.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CourtHearingSchedule.Data.CourtHearingScheduleContext _context;

        public CreateModel(CourtHearingSchedule.Data.CourtHearingScheduleContext context)
        {
            _context = context;
        }

        public IList<Hearing> Hearings { get; set; }
        public IList<SelectListItem> TimeSlots
        {
            get
            {
                IList<SelectListItem> TimeSlots = new List<SelectListItem>();
                TimeSlots.Add(new SelectListItem() { Text = "9:00 AM", Value = "9:00 AM" });
                TimeSlots.Add(new SelectListItem() { Text = "10:00 AM", Value = "10:00 AM" });
                TimeSlots.Add(new SelectListItem() { Text = "1:30 PM", Value = "1:30 PM" });
                TimeSlots.Add(new SelectListItem() { Text = "2:30 PM", Value = "2:30 PM" });

                return TimeSlots;
            }
        }

        public IActionResult OnGet()
        {
            ViewData["CaseId"] = new SelectList(_context.Cases, "Id", "Number");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["TimeSlots"] = new SelectList(TimeSlots, "Value", "Text");
            return Page();
        }

        [BindProperty]
        public Hearing NewHearing { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Hearings = await _context.Hearings
                .Include(h => h.Case)
                .Include(h => h.Department)
                .OrderBy(h => h.Number)
                .ThenBy(h => h.DateTime)
                .ThenBy(h => h.Type)
                .ThenBy(h => h.Case.Number).ToListAsync();

            if (Hearings[0].Number != null)
            {
                DateTime CurrentDateTime = DateTime.MinValue;
                int HearingNumber = 0;
                foreach (Hearing h in Hearings)
                {
                    if (h.DateTime > NewHearing.DateTime)
                    {
                        break;
                    }

                    if (CurrentDateTime != h.DateTime)
                    {
                        CurrentDateTime = h.DateTime;
                        HearingNumber = (HearingNumber / 1000 + 1) * 1000;
                    }

                    if (h.DepartmentId == NewHearing.DepartmentId)
                    {
                        HearingNumber++;
                    }
                }

                NewHearing.Number = HearingNumber;
            }

            _context.Hearings.Add(NewHearing);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
