using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserEntities
{
    public class UserActivityLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ActivityType { get; set; }

        public DateTimeOffset ActivityDate { get; set; }

        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")] //IP pattern
        public string IPAddress { get; set; }

        public string DeviceType { get; set; }

        public string DeviceOS { get; set; }

        public string BrowserType { get; set; }

        public string BrowserVersion { get; set; }

        public string Location { get; set; }
        public int? Duration { get; set; }
    }
}
