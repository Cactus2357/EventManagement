using System;
using System.Collections.Generic;

namespace EventManagement.Models;

public partial class ScheduleItem
{
    public int ItemId { get; set; }

    public int EventId { get; set; }

    public string Title { get; set; } = null!;

    public string? Speaker { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual Event Event { get; set; } = null!;
}
