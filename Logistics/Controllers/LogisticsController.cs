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
        
        [HttpPost("{customerId}/addresses")]
        public async Task<IActionResult> AddAddress(string customerId, [FromBody]AddAddressRequest req)
        {
            await _messageSession.SendLocal(new AddAddress(customerId, req.Address));
            return Accepted();
        }

    }

    public class AddAddressRequest
    {
        public string Address { get; set; }
    }

}
