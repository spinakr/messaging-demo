using System;
using NServiceBus;

namespace MessagingDemo.Orders.Sagas
{
    public class AddNewProduct : ICommand
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public AddNewProduct(Guid orderId, Guid productId)
        {
            ProductId = productId;
            OrderId = orderId;
        }
    }
}