using System;
using System.Threading.Tasks;
using NServiceBus;

namespace MessagingDemo.Payment.MessageHandlers
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

    public class AddProductHandler : IHandleMessages<AddNewProduct>
    {
        public Task Handle(AddNewProduct message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}