using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserCredentials
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string? EmailAddress { get; set; }

        [Required]
        [StringLength(255)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, " +
            "one numeric digit, and one special character.")]
        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(AccountStatusEnum))]
        public AccountStatusEnum AccountStatus { get; set; } = AccountStatusEnum.Active;

        [Required]
        public DateTimeOffset LastLoginDateTime { get; set; } = DateTimeOffset.MinValue;

        [Required]
        public DateTimeOffset AccountCreationDateTime { get; set; } = DateTimeOffset.Now;

        [StringLength(255)]
        public string PasswordResetToken { get; set; }

        public DateTimeOffset? PasswordResetExpirationDateTime { get; set; }

        // Removed navigation property to UserProfileInfo
    }
}
