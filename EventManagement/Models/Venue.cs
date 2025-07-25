using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    [Required(ErrorMessage = "Venue name is required.")]
    [StringLength(200, ErrorMessage = "Venue name cannot exceed 200 characters.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(300, ErrorMessage = "Address cannot exceed 300 characters.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Capacity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    public int Capacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
