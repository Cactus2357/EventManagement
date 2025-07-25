using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using EventManagement.Helpers;

namespace EventManagement.Pages.Events
{
    [Authorize(Roles = "organizer,admin")]
    public class ManageModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public ManageModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var q = _context.Events.AsQueryable();

            if (!User.IsAdmin())
            {
                var organizerId = User.GetCurrentUserId();
                q = q.Where(e => e.OrganizerId == organizerId);
            }

            Event = await q
                .Include(@event => @event.Organizer)
                .Include(@event => @event.Venue).ToListAsync();
        }
    }
}
