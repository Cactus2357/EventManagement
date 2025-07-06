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
    public class DetailsModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public DetailsModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public ScheduleItem ScheduleItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleitem = await _context.ScheduleItems.FirstOrDefaultAsync(m => m.ItemId == id);
            if (scheduleitem == null)
            {
                return NotFound();
            }
            else
            {
                ScheduleItem = scheduleitem;
            }
            return Page();
        }
    }
}
