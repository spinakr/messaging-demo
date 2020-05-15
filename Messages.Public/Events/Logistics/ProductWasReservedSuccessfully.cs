using NServiceBus;
using System;

namespace MessagingDemo.Messages.Public.Events.Logistics
{
    public interface ProductWasReservedSuccessfully : IEvent
    {
        Guid OrderId { get; set; }
        Guid ProductId { get; set; }
    }
}