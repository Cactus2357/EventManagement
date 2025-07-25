using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Venues
{
    [Authorize(Roles = "organizer,admin")]
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
            return Page();
        }

        [BindProperty]
        public Venue Venue { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Venues.Add(Venue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
