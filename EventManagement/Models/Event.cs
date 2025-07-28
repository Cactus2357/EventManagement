using EventManagement.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public partial class Event
{
    public int EventId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, ErrorMessage = "Title must be under 200 characters.")]
    public string Title { get; set; } = null!;

    [StringLength(1000, ErrorMessage = "Description must be under 1000 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Organizer is required.")]
    [Display(Name = "Organizer")]
    public int OrganizerId { get; set; }

    [Required(ErrorMessage = "Venue is required.")]
    [Display(Name = "Venue")]
    public int VenueId { get; set; }

    [Required(ErrorMessage = "Start Time is required.")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Start Time")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "End Time is required.")]
    [DataType(DataType.DateTime)]
    [Display(Name = "End Time")]
    [DateCompare("StartTime", ErrorMessage = "End Time must be after Start Time.")]
    public DateTime EndTime { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [RegularExpression("upcoming|completed|cancelled", ErrorMessage = "Status must be 'upcoming', 'completed', or 'cancelled'.")]
    public string Status { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User Organizer { get; set; } = null!;

    public virtual ICollection<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Venue Venue { get; set; } = null!;
}
