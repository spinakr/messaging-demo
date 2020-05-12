using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Orders
{
    public static class NServiceBusConfiguration
    {
        public static EndpointConfiguration ConfigureEndpoint(HostBuilderContext context)
        {
            var env = context.HostingEnvironment.EnvironmentName;
            var endpointConfiguration = new EndpointConfiguration($"guesswork.web_{env}");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            // var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            // transport.ConnectionString(context.Configuration.GetValue<string>("ASB_CON_STRING"));
            // transport.SubscriptionNameShortener(x => x.Split('.').Last());

            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString("host=rabbitmq");
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }}