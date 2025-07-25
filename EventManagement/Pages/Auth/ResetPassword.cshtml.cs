using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Pages.Auth
{
    public class ResetPasswordModel : PageModel
    {
        private readonly EventManagementContext _context;

        public ResetPasswordModel(EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public string Token { get; set; } = null!;

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string NewPassword { get; set; } = null!;

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        public string? StatusMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Email.Trim());

            if (user == null)
            {
                StatusMessage = "Invalid user.";
                return Page();
            }

            user.PasswordHash = NewPassword;
            await _context.SaveChangesAsync();

            StatusMessage = "Password reset successfully.";
            return RedirectToPage("/Auth/Signin");
        }
    }
}
