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
    public class DeleteModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public DeleteModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleitem = await _context.ScheduleItems.FindAsync(id);
            if (scheduleitem != null)
            {
                ScheduleItem = scheduleitem;
                _context.ScheduleItems.Remove(ScheduleItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
