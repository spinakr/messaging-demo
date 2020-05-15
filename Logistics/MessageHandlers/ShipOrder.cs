using MessagingDemo.Messages.Public.Events.Logistics;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace MessagingDemo.Logistics.MessageHandlers
{
    public class ShipOrder : ICommand
    {
        public Guid OrderId { get; set; }
    }

    public class ShipOrderHandler : IHandleMessages<ShipOrder>
    {
        public Task Handle(ShipOrder message, IMessageHandlerContext context)
        {
            return context.Publish<OrderWasShippedSuccessfully>(e =>
            {
                e.OrderId = message.OrderId;
            });
        }
    }
}
