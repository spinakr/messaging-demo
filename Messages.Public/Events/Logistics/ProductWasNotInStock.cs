using NServiceBus;
using System;

namespace MessagingDemo.Messages.Public.Events.Logistics
{
    public interface ProductWasNotInStock :IEvent
    {
        Guid OrderId { get; set; }
        Guid ProductId { get; set; }
    }
}