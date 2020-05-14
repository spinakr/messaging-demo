using System;

namespace MessagingDemo.Messages.Public.Events.Logistics
{
    public interface ProductWasReservedSuccessfully
    {
        Guid OrderId { get; set; }
        Guid ProductId { get; set; }
    }
}