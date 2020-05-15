using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagingDemo.Messages.Public.Events.Logistics;
using MessagingDemo.Messages.Public.Events.Orders;
using MessagingDemo.Messages.Public.Events.Payment;
using NServiceBus;

namespace MessagingDemo.Orders.Sagas
{
    public class OrderSaga : Saga<OrderSaga.OrderSagaData>,
        IAmStartedByMessages<InitOrder>,
        IHandleMessages<AddNewProduct>,
        IHandleMessages<StartOrderProcessing>,
        IHandleMessages<PaymentWasCollectedSuccessfully>,
        IHandleMessages<OrderWasShippedSuccessfully>
    {
        public class OrderSagaData : ContainSagaData
        {
            public Guid OrderId { get; set; }
            public string CustomerId { get; set; }
            public List<Guid> Products { get; set; }
            public bool Shipped { get; set; }
            public bool Payed { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper.ConfigureMapping<InitOrder>(m => m.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<AddNewProduct>(m => m.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<StartOrderProcessing>(m => m.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<PaymentWasCollectedSuccessfully>(m => m.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<OrderWasShippedSuccessfully>(m => m.OrderId).ToSaga(m => m.OrderId);
        }

        public Task Handle(InitOrder message, IMessageHandlerContext context)
        {
            Data.OrderId = message.OrderId;
            Data.Products = new List<Guid>();
            Data.Shipped = false;
            Data.Payed = false;
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

        public Task Handle(OrderWasShippedSuccessfully message, IMessageHandlerContext context)
        {
            Data.Shipped = true;
            if (Data.Shipped && Data.Payed) MarkAsComplete();
            return Task.CompletedTask;
        }

        public Task Handle(PaymentWasCollectedSuccessfully message, IMessageHandlerContext context)
        {
            Data.Payed = true;
            if (Data.Shipped && Data.Payed) MarkAsComplete();
            return Task.CompletedTask;
        }
    }
}