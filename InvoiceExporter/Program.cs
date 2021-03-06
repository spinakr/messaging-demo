﻿using Ice.NServiceBus.Defaults;
using MessagingDemo.CommonConfig;
using NServiceBus;
using NServiceBus.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingDemo.InvoiceExporter
{
    public class Program
    {
        private static IEndpointInstance _endpoint;

        static async Task Main(string[] args)
        {
            _endpoint = await Endpoint.Start(ConfigureEndpoint("Demo.InvoiceExporter"));
            var batchFile = await File.ReadAllLinesAsync("importFile.csv");
            var lines = batchFile.Skip(1);

            var tasks = new List<Task>();
            foreach (var line in lines)
            {
                var id = Guid.NewGuid();
                tasks.Add(_endpoint.SendLocal(new CreatInvoice { Id = id, Line = line }));
                tasks.Add(_endpoint.SendLocal(new CreateDocument { Id = id, Line = line }));
            }
            Status.ImportStarted();
            await Task.WhenAll(tasks);



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
                .Immediate(cfg => cfg.NumberOfRetries(3))
                .Delayed(cfg => cfg.NumberOfRetries(1).TimeIncrease(TimeSpan.FromSeconds(5)));

            endpointConfiguration.Pipeline.Remove("LogIceCorrelationIdBehavior");

            return endpointConfiguration;
        }
    }
}
