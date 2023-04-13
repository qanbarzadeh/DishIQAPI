using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserActivityLog
     {
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] move to Infra  
        public int ActivityLogId { get; set; }

        [Required]
        public int UserId { get; set; }

        // Navigation property to the related User entity
        //[ForeignKey("UserId")]  add to DbContext
        public User User { get; set; }

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
