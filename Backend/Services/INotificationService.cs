using Backend.Entities;

namespace Backend.Services
{
    public interface INotificationService
    {
        Task SendEmailToRecipients();
    }
}
