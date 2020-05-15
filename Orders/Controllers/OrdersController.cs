using System;
using System.Collections.Generic;
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
        private static readonly Stack<Guid> _guids = new Stack<Guid>(new[] {
            Guid.Parse("02210974-7916-494c-9e6e-6c980fc3bc17"),
            Guid.Parse("88496a14-a9f0-4f3d-bcab-4cee0d9a62d8"),
            Guid.Parse("f9a0ee02-b9c5-429a-b450-5f1b9c2adb0b"),
        });


        public OrdersController(ILogger<OrdersController> logger, IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(InitOrderRequest req)
        {
            var orderId = _guids.Pop();
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
