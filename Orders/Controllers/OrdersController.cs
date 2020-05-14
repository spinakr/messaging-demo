using System;
using System.Threading.Tasks;
using MessagingDemo.Orders.Sagas;
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
            await _messageSession.SendLocal(new InitOrder(orderId, req.CustomerId));
            return Accepted(orderId);
        }

        [HttpPost("{orderId}/products")]
        public async Task<IActionResult> AddProduct(Guid orderId, [FromBody]AddProductRequest req)
        {
            await _messageSession.SendLocal(new AddNewProduct(orderId, req.ProductId));
            return Accepted();
        }
        
        [HttpPost("{orderId}/process")]
        public async Task<IActionResult> StartOrderProcessing(Guid orderId)
        {
            await _messageSession.SendLocal(new StartOrderProcessing(orderId));
            return Accepted();
        }
    }

    public class InitOrderRequest
    {
        public string CustomerId { get; set; }
    }

    public class AddProductRequest
    {
        public Guid ProductId { get; set; }
    }
}
