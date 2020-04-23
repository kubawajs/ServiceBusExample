using ServiceBusExample.WebApp.Models;
using System.Threading.Tasks;

namespace ServiceBusExample.WebApp.Services
{
    public interface IServiceBusQueueService
    {
        Task SendMessageAsync(ServiceBusMessage serviceBusMessage);
    }
}