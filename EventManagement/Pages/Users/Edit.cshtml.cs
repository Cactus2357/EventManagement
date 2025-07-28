using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagement.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Pages.Users
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public EditModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User UserModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                id = User.GetCurrentUserId();
            }

            var user =  await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            if (!(User.IsAdmin() || User.GetCurrentUserId() == id)) return Forbid();

            UserModel = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserModel.UserId))
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
