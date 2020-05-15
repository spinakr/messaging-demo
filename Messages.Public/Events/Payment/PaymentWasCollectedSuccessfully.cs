using NServiceBus;
using System;

namespace MessagingDemo.Messages.Public.Events.Payment
{
    public interface PaymentWasCollectedSuccessfully : IEvent
    {
        Guid OrderId { get; set; }
    }
}