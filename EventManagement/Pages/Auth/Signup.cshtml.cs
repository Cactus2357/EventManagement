using EventManagement.Enums;
using EventManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Pages.Auth
{
    public class SignupModel : PageModel
    {
        private readonly EventManagementContext _context;

        public SignupModel(EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        [BindProperty]
        public string Role { get; set; } = UserRoles.Attendee;

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Events/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existing = _context.Users.FirstOrDefault(u => u.Email == Email.Trim());

            if (existing != null)
            {
                ModelState.AddModelError(string.Empty, "Email is already registered.");
                return Page();
            }

            var user = new User
            {
                Name = Name.Trim(),
                Email = Email.Trim(),
                PasswordHash = Password,
                Role = Role,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Auth/Signin");
        }
    }
}
