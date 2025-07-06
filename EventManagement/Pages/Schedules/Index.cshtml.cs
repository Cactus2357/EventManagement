using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;

namespace EventManagement.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public IndexModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IList<ScheduleItem> ScheduleItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ScheduleItem = await _context.ScheduleItems
                .Include(s => s.Event).ToListAsync();
        }
    }
}
