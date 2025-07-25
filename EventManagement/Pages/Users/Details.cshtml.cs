using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;

namespace EventManagement.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public DetailsModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;
        public IList<Event> Events { get; set; } = default!;
        public IList<Registration> Registrations { get; set; } = default!;

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

            User = user;
            Events = await _context.Registrations
                .Where(r => r.UserId == id)
                .Include(r => r.Ticket)
                .ThenInclude(t => t.Event)
                .Select(r => r.Ticket.Event)
                .ToListAsync();

            Registrations = await _context.Registrations
                .Where(r => r.UserId == id)
                .Include(r => r.Ticket).ThenInclude(t => t.Event)
                .ToListAsync();

            return Page();
        }
    }
}
