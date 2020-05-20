using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagingDemo.InvoiceExporterPrio
{
    public class CreatInvoice : ICommand
    {
        public Guid Id { get; set; }
        public string Line { get; set; }
    }

    public class CreatInvoiceHandler : IHandleMessages<CreatInvoice>
    {
        public Task Handle(CreatInvoice message, IMessageHandlerContext context)
        {
            var props = message.Line.Split(';');
            var toInvoice = new ToInvoice(props[0], props[1], props[2], int.Parse(props[3]));
            Status.InvoiceCreated();
            return Task.CompletedTask;
        }
    }
}
