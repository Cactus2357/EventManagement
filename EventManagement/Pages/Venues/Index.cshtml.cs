using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Venues
{
    [Authorize(Roles = "organizer,admin")]
    public class IndexModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public IndexModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IList<Venue> Venue { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Venue = await _context.Venues.ToListAsync();
        }
    }
}
