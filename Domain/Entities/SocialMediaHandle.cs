using Domain.Enums;

namespace Domain.Entities
{
    public class SocialMediaHandle
    {
        public int Id { get; set; }
        public SocialMediaTypeEnum Type { get; set; }                
        public string Handle { get; set; }
        public int UserProfileInfoId { get; set; }        
        //public virtual UserProfileInfo UserProfileInfo { get; set; }
    }
}
