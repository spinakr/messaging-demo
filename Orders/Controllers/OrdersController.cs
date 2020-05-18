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

            Guid.Parse("9560d054-151e-4aaf-8e61-429d5332c2fb"),
            Guid.Parse("c1ab1c14-b985-478a-824f-1d411a379850"),
            Guid.Parse("bbffd96a-4fb0-424f-aa01-da17863b2777"),
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
