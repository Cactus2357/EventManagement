using EventManagement.Helpers;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Schedules
{
    [Authorize(Roles = "organizer,admin")]
    public class CreateModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public CreateModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? eventId)
        {
            var items = _context.Events.AsQueryable();
            if (User.IsOrganizer())
            {
                items = items.Where(o => o.OrganizerId == User.GetCurrentUserId());
            }

            ViewData["EventId"] = new SelectList(items, "EventId", "Title", eventId);
            return Page();
        }

        [BindProperty]
        public ScheduleItem ScheduleItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ScheduleItems.Add(ScheduleItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
