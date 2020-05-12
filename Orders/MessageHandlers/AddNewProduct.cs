using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Orders.MessageHandlers
{
    public class AddNewProduct : ICommand
    {
        public Guid ProductId { get; set; }
        public AddNewProduct(Guid productId)
        {
            ProductId = productId;
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