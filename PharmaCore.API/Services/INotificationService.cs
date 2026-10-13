using System.Threading.Tasks;

namespace PharmaCore.API.Services
{
    public interface INotificationService
    {
        Task NotifyManagerAsync(string message);
    }
}