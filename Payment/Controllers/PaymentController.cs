using System;
using System.Threading.Tasks;
using MessagingDemo.Payment.MessageHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace MessagingDemo.Payment.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMessageSession _messageSession;

        public PaymentController(ILogger<PaymentController> logger, IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePaymentRequest(CreatePaymentRequest req)
        {
            var orderId = Guid.NewGuid();
            return Accepted(orderId);
        }

    }

    public class CreatePaymentRequest
    {
        public Guid OrderId { get; set; }
        public string BankAccount { get; set; }
        public string Ssn { get; set; }
    }

}
