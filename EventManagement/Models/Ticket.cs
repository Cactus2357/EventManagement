using EventManagement.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    [Required(ErrorMessage = "EventId is required.")]
    [Display(Name = "Event")]
    public int EventId { get; set; }

    [Required(ErrorMessage = "Ticket type is required.")]
    [StringLength(100, ErrorMessage = "Ticket type cannot exceed 100 characters.")]
    public string Type { get; set; } = null!;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Start Date")]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Expiry Date")]
    [DateCompare("StartDate", ErrorMessage = "Expiry date must be after start date.")]
    public DateTime? ExpiryDate { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
