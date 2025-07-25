using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using EventManagement.Helpers;
using Microsoft.AspNetCore.SignalR;
using EventManagement.Hubs;

namespace EventManagement.Pages.Events
{
    [Authorize(Roles = "organizer,admin")]
    public class EditModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;
        private readonly IHubContext<EventHub> _hubContext;

        public EditModel(EventManagement.Models.EventManagementContext context, IHubContext<EventHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            if (!User.IsEventOwner(@event.OrganizerId)) Forbid();

            Event = @event;
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Event.Organizer");
            ModelState.Remove("Event.Venue");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("EventReload");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.EventId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
