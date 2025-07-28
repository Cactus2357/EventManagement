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

namespace EventManagement.Pages.Tickets
{
    [Authorize(Roles = "organizer,admin")]
    public class CreateModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public CreateModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["EventId"] = new SelectList(_context.Events.Where(e => e.OrganizerId == User.GetCurrentUserId()), "EventId", "Title");

            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var data = await _context.Events
                .Where(e => e.EventId == 10)
                .Select(e => new
                {
                    VenueCapacity = e.Venue.Capacity,
                    TotalTickets = e.Tickets.Sum(t => t.Quantity)
                })
                .FirstOrDefaultAsync();

            int availableCapacity = data.VenueCapacity - data.TotalTickets;

            if (Ticket.Quantity > availableCapacity)
            {
                ModelState.AddModelError("Ticket.Quantity", "Ticket quantity exceeds venue's current capacity");
            }

            _context.Tickets.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
