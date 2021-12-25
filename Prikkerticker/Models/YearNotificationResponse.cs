using System.Collections.Generic;

namespace Prikkerticker.Models
{
    public class YearNotificationResponse
    {
        public bool Success { get; set; }

        public IEnumerable<string>? Years { get; set; }

        public YearNotificationResponse(IEnumerable<string> years)
        {
            Success = true;
            Years = years;
        }

        public YearNotificationResponse()
        {
            Success = false;
        }
    }
}
