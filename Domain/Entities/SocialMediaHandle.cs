using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class SocialMediaHandle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public SocialMediaTypeEnum Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Handle { get; set; }

        [ForeignKey("UserProfileInfo")]
        public int UserProfileInfoId { get; set; }

        // Navigation property to the related UserProfileInfo entity
        public virtual UserProfileInfo UserProfileInfo { get; set; }
    }
}
