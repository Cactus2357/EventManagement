using EventManagement.Helpers;
using EventManagement.Hubs;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace EventManagement.Pages.Events
{
    [Authorize(Roles = "organizer,admin")]
    public class CreateModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;
        private readonly IHubContext<EventHub> _hubContext;

        public CreateModel(EventManagement.Models.EventManagementContext context, IHubContext<EventHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Event.Organizer");
            ModelState.Remove("Event.Venue");
            ModelState.Remove("Event.Status");
            Event.OrganizerId = User.GetCurrentUserId();
            if (!ModelState.IsValid)
            {
                ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
                return Page();
            }

            if (Event.EndTime < DateTime.Now)
            {
                ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
                ModelState.AddModelError("Event.EndTime", "Invalid End Date");
                return Page();
            }

            Event.Status = "upcoming";

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("EventReload");

            return RedirectToPage("./Index");
        }
    }
}
