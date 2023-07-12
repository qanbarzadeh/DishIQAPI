using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Domain.Enums
{
    public enum SocialMediaTypeEnum
    {
        [EnumMember(Value = "Facebook")]
        [Display(Name = "Facebook")]
        Facebook = 1,

        [EnumMember(Value = "Twitter")]
        [Display(Name = "Twitter")]
        Twitter = 2,

        [EnumMember(Value = "Instagram")]
        [Display(Name = "Instagram")]
        Instagram = 3,

        [EnumMember(Value = "LinkedIn")]
        [Display(Name = "LinkedIn")]
        LinkedIn = 4,

        [EnumMember(Value = "Pinterest")]
        [Display(Name = "Pinterest")]
        Pinterest = 5,

        [EnumMember(Value = "Snapchat")]
        [Display(Name = "Snapchat")]
        Snapchat = 6,

        [EnumMember(Value = "TikTok")]
        [Display(Name = "TikTok")]
        TikTok = 7
    }
}
