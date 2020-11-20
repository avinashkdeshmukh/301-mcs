using Microsoft.Extensions.DependencyInjection;
using ServiceBusMessaging.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureDi(this IServiceCollection services, ServiceBusConfigurationsProvider serviceBusConfiguration)
        {
            //services.AddTransient<IServiceBusManager, ServiceBusManager>(fac =>
            //{
            //    var serviceBusTopicSender = new ServiceBusTopicSender(serviceBusConfiguration);
            //    var serviceBusTopicSubscription = new ServiceBusTopicSubscription(serviceBusConfiguration);

            //    return new ServiceBusManager(serviceBusTopicSender, serviceBusTopicSubscription);
            //});
        }
    }
}
