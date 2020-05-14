using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagingDemo.Messages.Public.Events.Orders;
using NServiceBus;

namespace MessagingDemo.Orders.Sagas
{
    public class OrderSaga : Saga<OrderSaga.OrderSagaData>,
        IAmStartedByMessages<InitOrder>,
        IHandleMessages<AddNewProduct>,
        IHandleMessages<StartOrderProcessing>
    {
        public class OrderSagaData : ContainSagaData
        {
            public Guid OrderId { get; set; }
            public string CustomerId { get; set; }
            public List<Guid> Products { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper.ConfigureMapping<InitOrder>(m => m.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<AddNewProduct>(m => m.OrderId).ToSaga(m => m.OrderId);
        }

        public Task Handle(InitOrder message, IMessageHandlerContext context)
        {
            Data.OrderId = message.OrderId;
            return Task.CompletedTask;
        }

        public Task Handle(AddNewProduct message, IMessageHandlerContext context)
        {
            Data.Products.Add(message.ProductId);
            return Task.CompletedTask;
        }

        public Task Handle(StartOrderProcessing message, IMessageHandlerContext context)
        {
            return context.Publish<OrderProcessingWasStarted>(e =>
            {
                e.OrderId = message.OrderId;
                e.Products = Data.Products;
                e.CustomerId = Data.CustomerId;
            });
        }
    }
}