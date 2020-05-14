using NServiceBus;
using System;
using System.Threading.Tasks;

namespace MessagingDemo.Payment.MessageHandlers
{
    public class CollectPayment : IHandleMessages<CollectPayment.CollectPaymentCommand>
    {
        private Random _random;

        public CollectPayment()
        {
            _random = new Random();
        }

        public class CollectPaymentCommand
        {
            public Guid OrderId { get; set; }

            public CollectPaymentCommand(Guid orderId)
            {
                OrderId = orderId;
            }
        }

        public class CollectPaymentResponse
        {
        }

        public Task Handle(CollectPaymentCommand message, IMessageHandlerContext context)
        {
            if (_random.Next(1, 10) < 8) throw new UnreliablePaymentProviderException();
            return context.Reply(new CollectPaymentResponse());
        }

        public class UnreliablePaymentProviderException : Exception
        {
        }
    }
}
