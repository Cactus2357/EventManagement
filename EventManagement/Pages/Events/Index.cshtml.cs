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
    public class IndexModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public IndexModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IList<Event> UpcommingEvents { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 12;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public async Task OnGetAsync()
        {
            var q = _context.Events
                .Where(@e => e.Status.Equals("upcoming"))
                .Include(@event => @event.Organizer).Include(@event => @event.Venue)
                .AsQueryable();

            Count = await q.CountAsync();

            UpcommingEvents = await q
                .OrderBy(e => e.StartTime)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
