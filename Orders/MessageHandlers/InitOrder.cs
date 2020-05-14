using System;
using System.Threading.Tasks;
using NServiceBus;

namespace MessagingDemo.Orders.MessageHandlers
{
    public class InitOrder : ICommand
    {
        public Guid OrderId { get; set; }
        public InitOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }

    public class InitOrderHandler : IHandleMessages<InitOrder>
    {
        public Task Handle(InitOrder message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}