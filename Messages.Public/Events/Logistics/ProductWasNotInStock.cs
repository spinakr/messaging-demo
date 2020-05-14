using System;

namespace MessagingDemo.Messages.Public.Events.Logistics
{
    public interface ProductWasNotInStock
    {
        Guid OrderId { get; set; }
        Guid ProductId { get; set; }
    }
}