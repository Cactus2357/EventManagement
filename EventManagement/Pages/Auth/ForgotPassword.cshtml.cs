using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Pages.Auth
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly EventManagementContext _context;

        public ForgotPasswordModel(EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? StatusMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = _context.Users.FirstOrDefault(u => u.Email == Email.Trim());

            if (user == null)
            {
                StatusMessage = "If this email is registered, a reset link will be sent.";
                return Page();
            }

            var token = Guid.NewGuid().ToString();

            return RedirectToPage("/Auth/ResetPassword", new { email = user.Email, token });
        }
    }
}
