using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceBusExample.WebApp.Models;
using ServiceBusExample.WebApp.Services;

namespace ServiceBusExample.WebApp.Controllers
{
    public class ServiceBusMessageController : Controller
    {
        private readonly ILogger<ServiceBusMessageController> _logger;
        private readonly IServiceBusQueueService _queueService;

        public ServiceBusMessageController(ILogger<ServiceBusMessageController> logger,
            IServiceBusQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        // GET: ServiceBusMessage/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: ServiceBusMessage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var message = new ServiceBusMessage
                {
                    Title = collection["Title"],
                    Message = collection["Message"]
                };

                _logger.LogInformation($"Message created: {message.Id}.");
        
                await _queueService.SendMessageAsync(message);

                _logger.LogInformation($"Message {message.Id} has been sent.");

                return RedirectToAction(nameof(Create));
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occured: {ex.Message}");
                return View();
            }
        }
    }
}