using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int EventId { get; set; }

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int? Rating { get; set; }

    [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
    public string? Comment { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? SubmittedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
