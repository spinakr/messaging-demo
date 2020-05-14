using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingDemo.Messages.Public.Events.Logistics;
using MessagingDemo.Messages.Public.Events.Orders;
using MessagingDemo.Messages.Public.Events.Payment;
using NServiceBus;
using static MessagingDemo.Payment.MessageHandlers.CollectPayment;

namespace MessagingDemo.Payment.MessageHandlers
{
    public class CollectPaymentSaga : Saga<CollectPaymentSaga.CollectPaymentSagaData>,
        IAmStartedByMessages<OrderProcessingWasStarted>,
        IHandleMessages<CollectPaymentResponse>,
        IHandleMessages<ProductWasReservedSuccessfully>,
        IHandleMessages<ProductWasNotInStock>
    {
        public class CollectPaymentSagaData : ContainSagaData
        {
            public Guid OrderId { get; set; }
            public Dictionary<Guid, bool?> Products { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CollectPaymentSagaData> mapper)
        {
            mapper.ConfigureMapping<OrderProcessingWasStarted>(m => m.OrderId).ToSaga(m => m.OrderId);
        }

        public Task Handle(OrderProcessingWasStarted message, IMessageHandlerContext context)
        {
            Data.Products = message.Products.ToDictionary(x => x, y => (bool?)null);
            return Task.CompletedTask;
        }

        public async Task Handle(CollectPaymentResponse message, IMessageHandlerContext context)
        {
            await context.Publish<PaymentWasCollectedSuccessfully>(e => { e.OrderId = Data.OrderId; });
            MarkAsComplete();
        }

        public Task Handle(ProductWasReservedSuccessfully message, IMessageHandlerContext context)
        {
            Data.Products[message.ProductId] = true;

            if (Data.Products.Values.All(p => p is object))
            {
                MarkAsComplete();
                return context.SendLocal(new CollectPaymentCommand(Data.OrderId));
            }

            return Task.CompletedTask;
        }

        public Task Handle(ProductWasNotInStock message, IMessageHandlerContext context)
        {
            Data.Products[message.ProductId] = false;
            if (Data.Products.Values.All(p => p is object))
            {
                MarkAsComplete();
                return context.SendLocal(new CollectPaymentCommand(Data.OrderId));
            }
            return Task.CompletedTask;
        }
    }


}