using System;
using System.Threading.Tasks;
using MessagingDemo.Messages.Public.Events.Payment;
using NServiceBus;

namespace MessagingDemo.Payment.MessageHandlers
{
    public class CollectPayment : ICommand
    {
        public Guid OrderId { get; set; }

        public CollectPayment(Guid orderId)
        {
            OrderId = orderId;
        }
    }

    public class CollectPaymentHandler : IHandleMessages<CollectPayment>
    {
        private readonly Random _random;

        public CollectPaymentHandler()
        {
            _random = new Random();    
        }
        
        public Task Handle(CollectPayment message, IMessageHandlerContext context)
        {
            if (_random.Next(1, 10) < 5) throw new UnreliablePaymentProviderException();
            return context.Publish<PaymentWasCollectedSuccessfully>(e => { e.OrderId = message.OrderId; });
        }
    }

    public class UnreliablePaymentProviderException : Exception
    {
    }
}