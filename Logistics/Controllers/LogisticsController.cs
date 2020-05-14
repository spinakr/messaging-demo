using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingDemo.Logistics.MessageHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace MessagingDemo.Logistics.Controllers
{
    [ApiController]
    [Route("api/logistics")]
    public class LogisticsController : ControllerBase
    {
        private readonly ILogger<LogisticsController> _logger;
        private readonly IMessageSession _messageSession;

        public LogisticsController(ILogger<LogisticsController> logger, IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }
        
        [HttpPost]
        public async Task<IActionResult> ReserveProducts(ReserveProductsRequest req)
        {
            await _messageSession.SendLocal(new ReserveProducts(req.OrderId, req.ProductIds));
            return Accepted();
        }

    }

    public class ReserveProductsRequest
    {
        public Guid OrderId { get; set; }
        public List<Guid> ProductIds { get; set; }
    }

}
