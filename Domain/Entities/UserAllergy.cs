using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class UserAllergy
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // to move to FlunetAPI 
        public int Id { get; set; }

        [Required]
        //[ForeignKey("User")] Configuration to move to Fluent API 
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public AllergySeverityLevelEnum SeverityLevel { get; set; }

        // Navigation property to the related User entity
        public virtual User User { get; set; }

        // Timestamp for when the allergy was added
        [Required]
        public DateTime CreatedAt { get; set; }

        // Timestamp for when the allergy was last updated
        [Required]
        public DateTime UpdatedAt { get; set; }
    }

}

