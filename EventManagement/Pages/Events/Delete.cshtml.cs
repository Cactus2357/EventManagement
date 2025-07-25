using EventManagement.Helpers;
using EventManagement.Hubs;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Events
{
    [Authorize(Roles = "organizer,admin")]
    public class DeleteModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;
        private readonly IHubContext<EventHub> _hubContext;

        public DeleteModel(EventManagement.Models.EventManagementContext context, IHubContext<EventHub> hubContext)
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
            else
            {
                Event = @event;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                if (!User.IsEventOwner(@event.OrganizerId)) Forbid();

                Event = @event;
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("EventReload");
            }

            return RedirectToPage("./Index");
        }
    }
}
