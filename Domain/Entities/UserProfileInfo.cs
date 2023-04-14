using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
     public class UserProfileInfo
    {
        //[Key]
        //[ForeignKey("User")] move to FluentAPI 
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(255)]
        public string ProfilePicture { get; set; }

        [StringLength(500)]
        public string Bio { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public DateTimeOffset LastLoginDate { get; set; }

        [Required]
        public DateTimeOffset AccountCreationDate { get; set; }

        [Required]
        public bool IsEmailVerified { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string SocialMediaHandles { get; set; }

        [StringLength(20)]
        public string LanguagePreference { get; set; }

        [Required]
        public bool NotificationSettings { get; set; }

        [StringLength(20)]
        public string SubscriptionStatus { get; set; }

        [StringLength(500)]
        public string PaymentInformation { get; set; }

        [StringLength(500)]
        public string UserActivityLog { get; set; }

        [Required]
        public bool IsSuspicious { get; set; }

        [Required]
        public bool IsBlacklisted { get; set; }       
    }
}
