using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserActivityLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ActivityType { get; set; }

        public DateTimeOffset ActivityDate { get; set; }

        public string IPAddress { get; set; }

        public string DeviceType { get; set; }

        public string DeviceOS { get; set; }

        public string BrowserType { get; set; }

        public string BrowserVersion { get; set; }

        public string Location { get; set; }
        public int? Duration { get; set; }
    }
}
