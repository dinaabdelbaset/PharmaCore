using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PharmaCore.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }
        public Task NotifyManagerAsync(string message)
        {
            _logger.LogWarning("Manager notification: {Message}", message);
            return Task.CompletedTask;
        }
    }
}