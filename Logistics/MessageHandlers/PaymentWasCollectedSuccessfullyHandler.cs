using MessagingDemo.Messages.Public.Events.Payment;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingDemo.Logistics.MessageHandlers
{
    public class PaymentWasCollectedSuccessfullyHandler : IHandleMessages<PaymentWasCollectedSuccessfully>
    {
        public Task Handle(PaymentWasCollectedSuccessfully message, IMessageHandlerContext context)
        {
            return context.SendLocal(new ShipOrder { OrderId = message.OrderId});
        }
    }
}
