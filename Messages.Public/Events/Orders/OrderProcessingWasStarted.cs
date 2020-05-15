using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingDemo.Messages.Public.Events.Orders
{
    public interface OrderProcessingWasStarted : IEvent
    {
        Guid OrderId { get; set; }
        List<Guid> Products { get; set; }
        string CustomerId { get; set; }
    }
}
