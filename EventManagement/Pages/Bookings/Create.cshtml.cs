using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Bookings
{
    [Authorize(Roles = "organizer,admin")]
    public class CreateModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public CreateModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketId");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Registrations.Add(Registration);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
