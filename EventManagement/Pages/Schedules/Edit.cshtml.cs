using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;

namespace EventManagement.Pages.Schedules
{
    public class EditModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public EditModel(EventManagement.Models.EventManagementContext context)
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

            var scheduleitem =  await _context.ScheduleItems.FirstOrDefaultAsync(m => m.ItemId == id);
            if (scheduleitem == null)
            {
                return NotFound();
            }
            ScheduleItem = scheduleitem;
           ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScheduleItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleItemExists(ScheduleItem.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScheduleItemExists(int id)
        {
            return _context.ScheduleItems.Any(e => e.ItemId == id);
        }
    }
}
