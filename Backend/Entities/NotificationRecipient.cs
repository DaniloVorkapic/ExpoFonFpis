namespace Backend.Entities
{
    public class NotificationRecipient : BaseEntity
    {
        public string Email { get; private set; }

        private NotificationRecipient(string email)
        {
            Email = email;
        }

        public static NotificationRecipient Create(string email)
        {
            return new NotificationRecipient(email);
        }
    }
}
