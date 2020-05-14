using System;
using NServiceBus;

namespace MessagingDemo.Orders.Sagas
{
    public class InitOrder : ICommand
    {
        public Guid OrderId { get; set; }
        public string CustomerId { get; set; }
        public InitOrder(Guid orderId, string customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}