using System;
using System.Threading.Tasks;
using MessagingDemo.Orders.MessageHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace MessagingDemo.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMessageSession _messageSession;

        public OrdersController(ILogger<OrdersController> logger, IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder(InitOrderRequest req)
        {
            var orderId = Guid.NewGuid();
            await _messageSession.SendLocal(new InitOrder(orderId));
            return Accepted(orderId);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequest req)
        {
            await _messageSession.SendLocal(new AddNewProduct(req.OrderId, req.ProductId));
            return Accepted();
        }
    }

    public class InitOrderRequest
    {
    }

    public class AddProductRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
