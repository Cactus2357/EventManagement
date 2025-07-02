using System;
using System.Collections.Generic;

namespace EventManagement.Models;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public int UserId { get; set; }

    public int TicketId { get; set; }

    public int Quantity { get; set; }

    public DateTime? RegisteredAt { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
