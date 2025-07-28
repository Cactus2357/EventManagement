using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Bookings
{
    [Authorize(Roles = "organizer,admin")]
    public class DeleteModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public DeleteModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FirstOrDefaultAsync(m => m.RegistrationId == id);

            if (registration == null)
            {
                return NotFound();
            }
            else
            {
                Registration = registration;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                Registration = registration;
                _context.Registrations.Remove(Registration);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
