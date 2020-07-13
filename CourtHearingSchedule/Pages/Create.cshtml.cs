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
        public IList<SelectListItem> Times
        {
            get
            {
                IList<SelectListItem> Times = new List<SelectListItem>();
                foreach (string Time in TimeSlots.Times)
                {
                    Times.Add(new SelectListItem() { Text = Time, Value = Time });
                }

                return Times;
            }
        }

        public IActionResult OnGet()
        {
            ViewData["CaseId"] = new SelectList(_context.Cases, "Id", "Number");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["TimeSlots"] = new SelectList(Times, "Value", "Text");
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
                int HearingNumber = (TimeSlots.Times.IndexOf(NewHearing.DateTime.ToString("t")) + 1) * 1000;
                foreach (Hearing h in Hearings)
                {
                    if (h.DepartmentId != NewHearing.DepartmentId)
                    {
                        continue;
                    }

                    if (h.Number > HearingNumber)
                    {
                        break;
                    }
                    else if (h.Number == HearingNumber)
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
