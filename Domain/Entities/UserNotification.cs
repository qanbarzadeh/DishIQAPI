namespace Domain.Entities
{
    public class UserNotification
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] move to FluentAPI 
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NotificationType { get; set; }
        public string NotificationText { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
