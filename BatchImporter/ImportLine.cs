using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatchImporter
{
    public class ImportLine : ICommand
    {
        public string Line { get; set; }
    }

    public class ImportLineHandler : IHandleMessages<ImportLine>
    {
        public Task Handle(ImportLine message, IMessageHandlerContext context)
        {
            var props = message.Line.Split(';');
            var toInvoice = new ToInvoice(props[0], props[1], props[2], int.Parse(props[3]));
            Console.WriteLine($"Created invoice for {toInvoice.Name}");
            return Task.CompletedTask;
        }
    }
}
