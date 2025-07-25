using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Pages.Bookings
{
    [Authorize(Roles = "organizer,admin")]
    public class IndexModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public IndexModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IList<Registration> Registration { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Registration = await _context.Registrations
                .Include(r => r.Ticket)
                .Include(r => r.User).ToListAsync();
        }
    }
}
