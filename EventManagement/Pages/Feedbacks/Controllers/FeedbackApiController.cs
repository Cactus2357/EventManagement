using EventManagement.Helpers;
using EventManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace EventManagement.Pages.Feedbacks.Controllers
{

    public class FeedbackDto
    {
        public int EventId { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string? Comment { get; set; } = null;
    }

    [Route("api/feedback")]
    [ApiController]
    public class FeedbackApiController : Controller
    {
        private readonly EventManagementContext _context;

        public FeedbackApiController(EventManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok(new { success = true, message = "test" });
        }

        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        public async Task<IActionResult> PostFeedback([FromForm] FeedbackDto feedbackDto)
        {
            if (_context.Feedbacks.Count(o => o.UserId == User.GetCurrentUserId()) > 0)
            {
                return Conflict(new { message = "You have already submitted a feedback for this event." });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            Feedback feedback = new Feedback
            {
                UserId = User.GetCurrentUserId(),
                EventId = feedbackDto.EventId,
                Rating = feedbackDto.Rating,
                Comment = feedbackDto.Comment?.Trim(),
                SubmittedAt = DateTime.Now
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
