using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityLogId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        

        [Required]
        [MaxLength(50)]
        public string ActivityType { get; set; }

        [Required]
        public DateTimeOffset ActivityDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string IPAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeviceType { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeviceOS { get; set; }

        [Required]
        [MaxLength(50)]
        public string BrowserType { get; set; }

        [Required]
        [MaxLength(50)]
        public string BrowserVersion { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        public int? Duration { get; set; }
    }
}
