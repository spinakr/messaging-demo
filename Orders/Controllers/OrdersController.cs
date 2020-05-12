using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Orders.MessageHandlers;

namespace Orders.Controllers
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
        public async Task<IActionResult> AddProduct(AddProductRequest req)
        {
            await _messageSession.SendLocal(new AddNewProduct(req.ProductId));
            return Accepted();
        }
    }

    public class AddProductRequest
    {
        public Guid ProductId { get; set; }
    }
}
