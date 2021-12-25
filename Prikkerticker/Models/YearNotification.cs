using System;
using System.Collections.Generic;

namespace Prikkerticker.Models;

public class YearNotification
{
    public YearNotification(IEnumerable<int> years, DateTime createdAt)
    {
        Years = years;
        CreatedAt = createdAt;
    }

    public IEnumerable<int> Years { get; set; }
    public DateTime CreatedAt { get; set; }
}
