using NServiceBus;
using System;

namespace MessagingDemo.Messages.Public.Events.Logistics
{
    public interface OrderWasShippedSuccessfully : IEvent
    {
        Guid OrderId { get; set; }
    }
}
