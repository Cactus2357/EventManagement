using EventManagement.Helpers;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Registrations
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public CreateModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;

        public Event? Event { get; set; } = default;

        public IList<Ticket> Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int eventId)
        {
            Event = _context.Events.FirstOrDefault(e => e.EventId == eventId);

            if (Event == null)
            {
                return NotFound();
            }

            if (User.GetCurrentUserId() == Event.OrganizerId)
            {
                return Forbid();
            }

            Ticket = await _context.Tickets
                .Where(t => t.EventId == Event.EventId && t.Quantity > 0)
                .Include(t => t.Registrations)
                .ToListAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Registration.UserId");
            ModelState.Remove("Registration.RegisteredAt");
            ModelState.Remove("Registration.Ticket");
            ModelState.Remove("Registration.User");

            var ticket = await _context.Tickets.Include(t => t.Registrations).FirstOrDefaultAsync(t => t.TicketId == Registration.TicketId);

            if (ticket == null)
            {
                return NotFound("Selected ticket not found.");
            }

            Event = await _context.Events.FirstOrDefaultAsync(e => e.EventId == ticket.EventId);
            Ticket = await _context.Tickets.Where(t => t.EventId == ticket.EventId && t.Quantity > 0).Include(t => t.Registrations).ToListAsync();

            {
                if (ticket.StartDate.HasValue && ticket.StartDate > DateTime.Now)
                {
                    ModelState.AddModelError("Registration.TicketId", "Ticket registration has not started yet.");
                }

                if (ticket.ExpiryDate.HasValue && ticket.ExpiryDate < DateTime.Now)
                {
                    ModelState.AddModelError("Registration.TicketId", "Ticket registration has ended.");
                }

                if (Registration.Quantity > (ticket.Quantity - ticket.Registrations.Count))
                {
                    ModelState.AddModelError("Registration.Quantity", "Requested quantity exceeds availability.");
                }

            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Registration.UserId = User.GetCurrentUserId();
            _context.Registrations.Add(Registration);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Events/Details", new { id = Event.EventId });
        }
    }
}
