namespace Domain.Entities.UserRegistration
{
    public class AuthUser
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ExternalLogin> ExternalLogins { get; set; } = new List<ExternalLogin>();
        public bool IsDeleted { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int Version { get; set; } = 1;
    }
}
