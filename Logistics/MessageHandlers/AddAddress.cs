using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;

namespace MessagingDemo.Logistics.MessageHandlers
{
    public class AddAddress : ICommand
    {
        public string CustomerId { get; set; }
        public string Address { get; set; }

        public AddAddress(string customerId, string address)
        {
            Address = address;
            CustomerId = customerId;
        }
    }

    public class AddAddressHandler : IHandleMessages<AddAddress>
    {

        public Task Handle(AddAddress message, IMessageHandlerContext context)
        {
            LogisticsDatabase.AddAddressToCustomer(message.CustomerId, message.Address);
            return Task.CompletedTask;
        }
    }
}