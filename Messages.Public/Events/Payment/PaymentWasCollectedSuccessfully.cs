using System;

namespace MessagingDemo.Messages.Public.Events.Payment
{
    public interface PaymentWasCollectedSuccessfully
    {
        Guid OrderId { get; set; }
    }
}