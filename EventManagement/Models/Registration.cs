using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

/// <summary>
/// Event registration history.
/// </summary>
public partial class Registration
{
    public int RegistrationId { get; set; }

    [Required(ErrorMessage = "User is required.")]
    [Display(Name = "User")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Ticket is required.")]
    [Display(Name = "Ticket")]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Registration Date")]
    public DateTime? RegisteredAt { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
