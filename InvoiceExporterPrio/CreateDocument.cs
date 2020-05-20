using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessagingDemo.InvoiceExporterPrio
{
    public class CreateDocument : ICommand
    {
        public Guid Id { get; set; }
        public string Line { get; set; }
    }

    public class CreateDocumentHandler : IHandleMessages<CreateDocument>
    {
        private static readonly Random _rnd = new Random();
        public async Task Handle(CreateDocument message, IMessageHandlerContext context)
        {
            await Task.Delay(200);
            var props = message.Line.Split(';');
            var toInvoice = new ToInvoice(props[0], props[1], props[2], int.Parse(props[3]));
            Status.DocumentCreated();
        }
    }
}
