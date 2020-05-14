using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingDemo.Orders.Sagas
{
    public class StartOrderProcessing : ICommand
    {
        public StartOrderProcessing(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
