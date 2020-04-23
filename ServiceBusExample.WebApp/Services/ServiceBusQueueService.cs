using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using ServiceBusExample.WebApp.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBusExample.WebApp.Services
{
    public class ServiceBusQueueService : IServiceBusQueueService
    {
        private const string _serviceBusConnectionString = "<your-service-bus-connection-string>";
        private const string _queueName = "<your-queue-name>";
        
        private readonly IQueueClient _queueClient;
        private readonly ILogger<IServiceBusQueueService> _logger;

        public ServiceBusQueueService(ILogger<IServiceBusQueueService> logger)
        {
            _queueClient = new QueueClient(_serviceBusConnectionString, _queueName);
            _logger = logger;
        }

        public async Task SendMessageAsync(ServiceBusMessage serviceBusMessage)
        {
            try
            {
                // Serialize message and send to the queue.
                string messageBody = JsonSerializer.Serialize(serviceBusMessage);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                // Log the body of the message to the console.
                _logger.LogInformation($"Sending message: {messageBody}");

                // Send the message to the queue.
                await _queueClient.SendAsync(message);

            }
            catch (Exception exception)
            {
                // Log errors
                _logger.LogError($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
