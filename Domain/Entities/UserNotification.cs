using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserNotification
    {

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] move to FluentAPI 
        public int NotificationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string NotificationType { get; set; }

        [Required]
        [MaxLength(500)]
        public string NotificationText { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

     
    }
}
