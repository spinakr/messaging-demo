using Microsoft.Extensions.Hosting;
using NServiceBus;

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

            // var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            // transport.ConnectionString(context.Configuration.GetValue<string>("ASB_CON_STRING"));
            // transport.SubscriptionNameShortener(x => x.Split('.').Last());

            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString("");
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }}