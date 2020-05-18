using Ice.NServiceBus.Defaults;
using MessagingDemo.CommonConfig;
using NServiceBus;
using NServiceBus.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BatchImporter
{
    partial class Program
    {
        private static IEndpointInstance _endpoint;

        static async Task Main(string[] args)
        {
            Console.WriteLine("1: In memory, 2: message based");
            var key = Console.ReadLine();



            _endpoint = await Endpoint.Start(ConfigureEndpoint("BatchImproter"));
            var batchFile = await File.ReadAllLinesAsync("importFile.csv");
            var lines = batchFile.Skip(1);

            if (key == "1")
            {
                foreach (var line in lines)
                {
                    var props = line.Split(';');
                    var toInvoice = new ToInvoice(props[0], props[1], props[2], int.Parse(props[3]));
                    Console.WriteLine($"Created invoice for {toInvoice.Name}");
                }
            }else if (key == "2")
            {
                var tasks = new List<Task>();
                foreach (var line in lines)
                {
                    tasks.Add(_endpoint.SendLocal(new ImportLine { Line = line }));
                }
                await Task.WhenAll(tasks);
            }



            Console.ReadLine();
        }

        public static EndpointConfiguration ConfigureEndpoint(string endpointName)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            endpointConfiguration.ApplyIceReceiveEndpointDefaults(
                      new SendAndReceiveSettings()
                          .WithConnectionString(Environment.GetEnvironmentVariable("ASB_CONNECTION_STRING"))
                          .SetLocalDevelopment(true));

            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
            persistence.SagaStorageDirectory("../SagaStorage");
            endpointConfiguration.DisableFeature<Outbox>();

            endpointConfiguration.Recoverability()
                .Immediate(cfg => cfg.NumberOfRetries(1))
                .Delayed(cfg => cfg.NumberOfRetries(0));

            endpointConfiguration.Pipeline.Remove("LogIceCorrelationIdBehavior");

            return endpointConfiguration;
        }
    }
}
