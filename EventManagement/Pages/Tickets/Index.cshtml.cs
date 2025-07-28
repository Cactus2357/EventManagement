using EventManagement.Helpers;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Tickets
{
    [Authorize(Roles = "organizer,admin")]
    public class IndexModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public IndexModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var q = _context.Tickets.AsQueryable();

            if (User.IsOrganizer())
            {
                q = q.Where(t => t.Event.OrganizerId == User.GetCurrentUserId());
            }

            Ticket = await q
                .Include(t => t.Event)
                .OrderBy(t => t.EventId)
                .ToListAsync();
        }
    }
}
