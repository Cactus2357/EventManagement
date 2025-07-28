using EventManagement.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public partial class ScheduleItem
{
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Event is required")]
    [Display(Name = "Event")]
    public int EventId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = null!;

    [StringLength(100, ErrorMessage = "Speaker name can't exceed 100 characters.")]
    public string? Speaker { get; set; }

    [Required]
    [Display(Name = "Start Time")]
    public DateTime StartTime { get; set; }

    [Required]
    [Display(Name = "End Time")]
    [DateCompare("StartTime", ErrorMessage = "End Time must be after Start Time.")]
    public DateTime EndTime { get; set; }

    public virtual Event Event { get; set; } = null!;
}
