using System;

namespace ServiceBusExample.WebApp.Models
{
    public class ServiceBusMessage
    {
        public Guid Id { get; }
        public string Title { get; set; }
        public string Message { get; set; }

        public ServiceBusMessage()
        {
            Id = Guid.NewGuid();
        }
    }
}
