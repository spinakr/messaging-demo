using MessagingDemo.Messages.Public.Events.Logistics;
using MessagingDemo.Messages.Public.Events.Orders;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingDemo.Logistics.MessageHandlers
{
    public class OrderProcessingWasStartedHandler : IHandleMessages<OrderProcessingWasStarted>
    {
        public async Task Handle(OrderProcessingWasStarted message, IMessageHandlerContext context)
        {
            foreach (var product in message.Products)
            {
                if (LogisticsDatabase.ReserveProduct(product))
                {
                    await context.Publish<ProductWasReservedSuccessfully>(e =>
                    {
                        e.OrderId = message.OrderId;
                        e.ProductId = product;
                    });
                }
                else { 
                    await context.Publish<ProductWasNotInStock>(e =>
                    {
                        e.OrderId = message.OrderId;
                        e.ProductId = product;
                    });
                };
            }
        }
    }
}
