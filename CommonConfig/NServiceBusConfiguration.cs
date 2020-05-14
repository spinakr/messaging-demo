using Ice.NServiceBus.Defaults;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Features;
using System;

namespace MessagingDemo.CommonConfig
{
    public class NServiceBusConfiguration
    {
        private string _endpointName;
        public NServiceBusConfiguration(string endpointName)
        {
            _endpointName = endpointName;
        }

        public EndpointConfiguration ConfigureEndpoint(HostBuilderContext context)
        {
            var endpointConfiguration = new EndpointConfiguration(_endpointName);
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            endpointConfiguration.ApplyIceReceiveEndpointDefaults(
                      new SendAndReceiveSettings()
                          .WithConnectionString(Environment.GetEnvironmentVariable("ASB_CONNECTION_STRING"))
                          .SetLocalDevelopment(true));

            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.DisableFeature<Outbox>();

            endpointConfiguration.Recoverability()
                .Immediate(cfg => cfg.NumberOfRetries(3))
                .Delayed(cfg => cfg.NumberOfRetries(0));

            return endpointConfiguration;
        }
    }
}