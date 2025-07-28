using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;
using EventManagement.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public DetailsModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public User CurrentUser { get; set; } = default!;
        public IList<Registration> Registrations { get; set; } = default!;
        public IList<Event> RegisteredEvents { get; set; } = default!;
        public IList<Event> ManagedEvents { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            CurrentUser = user;
            Registrations = await _context.Registrations
                .Where(r => r.UserId == id)
                .Include(r => r.Ticket).ThenInclude(t => t.Event)
                .ToListAsync();

            RegisteredEvents = await _context.Events
                .Include(e => e.Tickets).ThenInclude(t => t.Registrations)
                .Where(e => e.Tickets.Any(t => t.Registrations.Any(r => r.UserId == id)))
                .ToListAsync();

            ManagedEvents = await _context.Events
                .Where(e => e.OrganizerId == id)
                .ToListAsync();

            return Page();
        }
    }
}
