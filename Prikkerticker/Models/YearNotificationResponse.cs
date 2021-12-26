namespace Prikkerticker.Models
{
    public class YearNotificationResponse
    {
        public bool Success { get; set; }
        public YearNotification? YearNotification { get; set; }

        public YearNotificationResponse(YearNotification? yearNotification)
        {
            YearNotification = yearNotification;
            Success = true;
        }

        public YearNotificationResponse()
        {
            Success = false;
        }
    }
}
