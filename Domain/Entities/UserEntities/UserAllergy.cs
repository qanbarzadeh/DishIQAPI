using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities.UserEntities
{
    public class UserAllergy
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public AllergySeverityLevelEnum SeverityLevel { get; set; }

        // Timestamp for when the allergy was added
        public DateTime CreatedAt { get; set; }

        // Timestamp for when the allergy was last updated
        public DateTime UpdatedAt { get; set; }
    }
}
