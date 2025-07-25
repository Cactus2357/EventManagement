using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;

namespace EventManagement.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public DetailsModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public Event Event { get; set; } = default!;

        public IList<Ticket> Tickets { get; set; } = default!;
        public IList<Feedback> Feedbacks { get; set; } = default!;
        public IList<ScheduleItem> ScheduleItems { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.Include(e => e.Organizer).Include(e => e.Venue).FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            Tickets = await _context.Tickets.Where(o => o.EventId == id).ToListAsync();
            Feedbacks = await _context.Feedbacks.Where(o => o.EventId == id).Include(o => o.User).ToListAsync();
            ScheduleItems = await _context.ScheduleItems.Where(o => o.EventId == id).ToListAsync();

            Event = @event;
            return Page();
        }
    }
}
