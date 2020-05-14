using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;

namespace MessagingDemo.Logistics.MessageHandlers
{
    public class ReserveProducts : ICommand
    {
        public Guid OrderId { get; set; }
        public List<Guid> ProductIds { get; set; }

        public ReserveProducts(Guid orderId, List<Guid> productIds)
        {
            ProductIds = productIds;
            OrderId = orderId;
        }
    }

    public class ReserveProductsHandler : IHandleMessages<ReserveProducts>
    {
        private readonly Random _random;

        public ReserveProductsHandler()
        {
            _random = new Random();    
        }
        
        public Task Handle(ReserveProducts message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}