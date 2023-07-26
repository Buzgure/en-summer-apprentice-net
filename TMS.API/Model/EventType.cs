using System;
using System.Collections.Generic;

namespace TMS.API.Model;

public partial class EventType
{
    public int EventTypeId { get; set; }

    public string? EventTypeName { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
