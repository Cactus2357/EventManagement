using EventManagement.Helpers;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Pages.Feedbacks
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly EventManagement.Models.EventManagementContext _context;

        public CreateModel(EventManagement.Models.EventManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Status");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Feedbacks.Add(Feedback);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAjaxFeedbackAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var userId = User.GetCurrentUserId();

            var feedback = new Feedback
            {
                EventId = Feedback.EventId,
                Rating = Feedback.Rating,
                Comment = Feedback.Comment,
                SubmittedAt = DateTime.Now,
                UserId = userId
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

    }
}
