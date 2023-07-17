using Domain.Enums;

namespace Domain.Entities.UserEntities
{
    public class UserProfileInfo
    {
        //[Key]
        //[ForeignKey("User")] move to FluentAPI 
        //public int UserId { get; set; }
            
        public int Id { get; set;  }

        public string Username { get; set; }


        public string EmailAddress { get; set; }


        public string Password { get; set; }


        public string FullName { get; set; }


        public Gender Gender { get; set; }


        public DateTime DateOfBirth { get; set; }

        public string ProfilePicture { get; set; }

        public string Bio { get; set; }

        public string Location { get; set; }

        public DateTimeOffset LastLoginDate { get; set; }

        public DateTimeOffset AccountCreationDate { get; set; }

        public bool IsEmailVerified { get; set; }

        public string PhoneNumber { get; set; }

        public string SocialMediaHandle { get; set; }

        public string LanguagePreference { get; set; }

        public bool NotificationSettings { get; set; }

        public string SubscriptionStatus { get; set; }

        public string PaymentInformation { get; set; }

        public string UserActivityLog { get; set; }

        public bool IsSuspicious { get; set; }

        public bool IsBlacklisted { get; set; }

        public virtual ICollection<SocialMediaHandle>? SocialMediaHandles { get; set; }
    }
}
